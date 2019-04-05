using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Constants;
using TelstarCES.Controllers;
using TelstarCES.Data.Models;
using TelstarCES.Enums;
using TelstarCES.Models;

namespace TelstarCES.Services
{
    public class RouteService : IRouteService
    {
        private const int maxIterations = 1000;

        private readonly List<Connection> _visited = new List<Connection>(40);
        private readonly List<PathNode> _openNodes = new List<PathNode>(40);

        private readonly IDataService _dataService;
        private readonly RouteController _routeController;

        private City _destination;
        private PathNode _currentNode;
        private Segment[] _lastPath;

        private FilterType _filterType;
        private float _weight;
        private string _parcelType;

        public RouteService(IDataService dataService, RouteController routeController)
        {
            _dataService = dataService;
            _routeController = routeController;
        }

        public async Task<RouteViewModel> CalculateRoute(string fromName, string toName, string parcelType, float weight, bool recommended, FilterType filterType)
        {
            var fromCity = await _dataService.GetCity(fromName);
            if (fromCity == null)
            {
                throw new ArgumentException("fromName");
            }

            var toCity = await _dataService.GetCity(toName);
            if (toCity == null)
            {
                throw new ArgumentException("toName");
            }

            _weight = weight;
            _parcelType = parcelType;
            _filterType = filterType;
            var path = await FindPath(fromCity, toCity);
            return new RouteViewModel
            {
                Segments = path,
                TotalDuration = path.Sum(p => p.Duration),
                TotalPrice = path.Sum(p => p.Price) + (recommended ? 10f : 0f)
            };
        }

        public async Task<Segment[]> FindPath(City origin, City destination)
        {
            // Store the starting cell as a path node and mark it as visited, and save the destination cell for later
            _destination = destination;
            _currentNode = new PathNode
            {
                City = origin,
            };
            
            // keep iterating until a path is found or we have surpassed the max iterations
            var counter = 0;
            while (true)
            {
                if (await FindPathIteration())
                {
                    break;
                }

                if (counter++ > maxIterations)
                {
                    // "fail-safe" to avoid the pathfinder from consuming too many resources (avoid too many iterations), especially for cases where there is no possible path
                    Console.WriteLine(ToString() + " could not find a path. Max tries exceeded");
                    break;
                }
            }

            // Reset everything for next time a path is needed
            Reset();

            // return the identified path
            return _lastPath;
        }

        private async Task<bool> FindPathIteration()
        {
            // Get all the current node's city's connections
            var connections = await _dataService.GetConnections(_currentNode.City.CityId);
            var count = connections.Length;
            if (count == 0)
            {
                Console.WriteLine($"{ToString()} could not find a path. No connections to start city ({_currentNode.City.CityName}) found");
                return true;
            }

            // Iterate through the list of city connections
            foreach (var connection in connections)
            {
                // get the city id on the other side of the connection
                var cityId = _currentNode.City.CityId == connection.City1Id ? connection.City2Id : connection.City1Id; 
                if (cityId == _destination.CityId)
                {
                    BuildPath();
                    return true;
                }

                if (_visited.Contains(connection))
                {
                    // the connection has already been visited previously
                    continue;
                }

                // Calculate the cost for this connection and add it as a path node to the open collection
                var city = await _dataService.GetCity(cityId);
                var cost = await GetCost(connection);
                var node = new PathNode
                {
                     Parent = _currentNode,
                     City = city,
                     Connection = connection,
                     Cost = cost
                };

                _openNodes.Add(node);
            }

            // Iterate through all open nodes in order to identify the one with the lowest cost
            var openNodesCount = _openNodes.Count;
            var lowestCost = float.MaxValue;
            for (var j = 0; j < openNodesCount; j++)
            {
                var node = _openNodes[j];
                var cost = node.Cost;
                if (cost < lowestCost)
                {
                    lowestCost = cost;
                    _currentNode = node;
                }
            }

            // The current node is no longer in the open collection - it is the next one to be evaluated, and thus it has also been visited now
            _openNodes.Remove(_currentNode);
            _visited.Add(_currentNode.Connection);

            // Still iterating
            return false;
        }

        private void BuildPath()
        {
            // Build the path by going backwards from the destination city - each path node knows about its 'parent' so use this for iterating
            var path = new Segment[_visited.Count];
            var current = new PathNode
            {
                City = _destination,
                Parent = _currentNode,
            };

            int index = 0;
            while (current != null)
            {
                path[index++] = new Segment
                {
                    ToCity = current.City.CityName,
                    FromCity = current.Parent.City.CityName,
                    Duration = current.Connection.Duration,
                    Price = current.Connection.Price,
                    Provider = current.Connection.Provider
                };

                current = current.Parent;
            }

            // Reverse the order of the path to make it start from the intended origin
            Array.Reverse(path);
            _lastPath = path;
        }

        private void Reset()
        {
            // Reset everything but the last generated path (since it may need to be returned)
            _currentNode = null;
            _destination = null;
            _visited.Clear();
            _openNodes.Clear();
        }

        private async Task<float> GetCost(Connection connection)
        {
            var baseCost = 0f;
            if (string.Equals(connection.Provider, ProviderNames.EastIndia, StringComparison.InvariantCultureIgnoreCase))
            {
                var fromCity = await _dataService.GetCity(connection.City1Id);
                var toCity = await _dataService.GetCity(connection.City2Id);
                var data = await _routeController.GetExternalEITC(fromCity.CityName, toCity.CityName, (int)_filterType,
                    _weight, _parcelType);

                baseCost = (float) (_filterType == FilterType.Cheapest ? data.Price : data.Duration);
                return baseCost * RouteMetrics.CompetitorBiasFactor;
            }
            else if (string.Equals(connection.Provider, ProviderNames.Oceanic, StringComparison.InvariantCultureIgnoreCase))
            {
                var fromCity = await _dataService.GetCity(connection.City1Id);
                var toCity = await _dataService.GetCity(connection.City2Id);
                var data = await _routeController.GetExternalIOA(fromCity.CityName, toCity.CityName, (int) _filterType,
                    _weight, _parcelType);

                baseCost = (float) (_filterType == FilterType.Cheapest ? data.Price : data.Duration);
                return baseCost * RouteMetrics.CompetitorBiasFactor;
            }

            // the base cost is either the price or the duration depending on the chosen filter type
            baseCost = _filterType == FilterType.Cheapest ? connection.Price : connection.Duration;

            // the heuristic factor biases towards Telstar routes
            return baseCost * RouteMetrics.TelstarProviderBiasFactor;
        }

        private class PathNode
        {
            public PathNode Parent;
            public float Cost;
            public City City;
            public Connection Connection;
        }
    }
}
