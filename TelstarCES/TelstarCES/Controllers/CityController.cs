using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IDataService _dataService;

        public CityController(ApplicationDbContext db)
        {
          _dataService = new DataService(db);
        }

        [HttpGet]
        [Route("Cities")]
        public async Task<City[]> Cities()
        {
            return await _dataService.GetCities();
        }

        [HttpGet]
        public async Task<City> Get(int cityId)
        {
            if (cityId < 0)
            {
                return null;
            }

            return await _dataService.GetCity(cityId);
        }

        [HttpPut]
        public async Task<bool> Put(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.CityName))
            {
                return false;
            }

            if (_dataService.GetCity(city.CityId) != null)
            {
                return false;
            }

            return await _dataService.UpdateCity(city);
        }

        [HttpPost]
        public async Task<bool> Post(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.CityName))
            {
                return false;
            }

            if (_dataService.GetCity(city.CityId) != null)
            {
                return false;
            }

            return await _dataService.AddCity(city);
        }

        [HttpDelete]
        public async Task<bool> Delete(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.CityName))
            {
                return false;
            }

            return await _dataService.DeleteCity(city);
        }
    }
}