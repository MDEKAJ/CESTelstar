using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IDataService _dataService = new DataService();
        public async Task<City[]> Cities()
        {
            return await _dataService.GetCities();
        }

        public async Task<City> Get(int cityId)
        {
            return await _dataService.GetCity(cityId);
        }

        public async Task<bool> Put(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.CityName))
            {
                return false;
            }

            return await _dataService.UpdateCity(city);
        }

        public async Task<bool> Post(City city)
        {
            if (string.IsNullOrWhiteSpace(city?.CityName))
            {
                return false;
            }

            return await _dataService.AddCity(city);
        }

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