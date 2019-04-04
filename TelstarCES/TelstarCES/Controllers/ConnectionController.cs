using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IDataService _dataService = new DataService();

        public async Task<Connection[]> Connections()
        {
            return await _dataService.GetConnections();
        }

        public async Task<Connection> Get(int connectionId)
        {
            return await _dataService.GetConnection(connectionId);
        }

        public async Task<bool> Put(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1 == null || connection.City2 == null)
            {
                return false;
            }

            if (connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            return await _dataService.UpdateConnection(connection);
        }

        public async Task<bool> Post(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1 == null || connection.City2 == null)
            {
                return false;
            }

            if (connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            return await _dataService.AddConnection(connection);
        }

        public async Task<bool> Delete(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1 == null || connection.City2 == null)
            {
                return false;
            }

            if (connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            return await _dataService.DeleteConnection(connection);
        }

    }
}