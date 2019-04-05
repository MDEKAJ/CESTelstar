using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ConnectionController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
        }

        [HttpGet]
        [Route("Connections")]
        public async Task<Connection[]> Connections()
        {
            return await _dataService.GetConnections();
        }

        [HttpGet]
        public async Task<Connection> Get(int connectionId)
        {
            if (connectionId < 0)
            {
                return null;
            }

            return await _dataService.GetConnection(connectionId);
        }

        [HttpGet]
        [Route("GetForCity")]
        public async Task<Connection[]> GetForCity(int cityId)
        {
            if (cityId < 0)
            {
                return null;
            }

            return await _dataService.GetConnections(cityId);
        }

        [HttpPut]
        public async Task<bool> Put(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1Id < 0 || connection.City2Id < 0 || connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            if (_dataService.GetCity(connection.City1Id) == null ||
                _dataService.GetCity(connection.City2Id) == null)
            {
                return false;
            }

            return await _dataService.UpdateConnection(connection);
        }

        [HttpPost]
        public async Task<bool> Post(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1Id < 0 || connection.City2Id < 0 || connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            if (_dataService.GetCity(connection.City1Id) == null ||
                _dataService.GetCity(connection.City2Id) == null)
            {
                return false;
            }

            return await _dataService.AddConnection(connection);
        }

        [HttpDelete]
        public async Task<bool> Delete(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection?.Provider))
            {
                return false;
            }

            if (connection.City1Id < 0 || connection.City2Id < 0 || connection.Duration < 0 || connection.Price < 0)
            {
                return false;
            }

            if (_dataService.GetCity(connection.City1Id) == null ||
                _dataService.GetCity(connection.City2Id) == null)
            {
                return false;
            }

            return await _dataService.DeleteConnection(connection);
        }

    }
}