using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Enums;
using TelstarCES.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IRouteService _routeService;
        private readonly HttpClient client = new HttpClient();
        private const string OceanicAirlinesDomain = "http://wa-oadk.azurewebsites.net";
        private const string TradingCompanyDomain = "http://wa-eitdk.azurewebsites.net";

        public RouteController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
            _routeService = new RouteService(_dataService, this);
        }

        [HttpGet]
        public async Task<ExternalRouteModel> Index(string fromName, string toName, int filter, float weight, string parcelType)
        {
            var path = await _routeService.CalculateRoute(fromName, toName, parcelType, weight, false, (FilterType) filter);

            return new ExternalRouteModel
            {
                Price = (decimal)path.TotalPrice,
                Duration = path.TotalDuration,
                Valid = path.Segments?.Length > 0
            };
        }

        [HttpGet]
        [Route("GetExternalIOA")]
        public async Task<ExternalRouteModel> GetExternalIOA(string fromName, string toName, int filter, float weight, string parcelType)
        {
            try
            {
                var uri = new UriBuilder(
                    $"{OceanicAirlinesDomain}/api/route?fromName={Uri.EscapeDataString(fromName)}&toName={Uri.EscapeDataString(toName)}&parcelType={Uri.EscapeDataString(parcelType)}&weight={weight}&filter={filter}");
                var response = await client.GetAsync(uri.Uri.AbsoluteUri);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExternalRouteModel>(json);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("GetExternalEITC")]
        public async Task<ExternalRouteModel> GetExternalEITC(string fromName, string toName, int filter, float weight, string parcelType)
        {
            try
            {
                var uri = new UriBuilder(
                    $"{TradingCompanyDomain}/api/route/get?fromName={Uri.EscapeDataString(fromName)}&toName={Uri.EscapeDataString(toName)}&parcelType={Uri.EscapeDataString(parcelType)}&weight={weight}&filter={filter}");
                var response = await client.GetAsync(uri.Uri.AbsoluteUri);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExternalRouteModel>(json);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("Calculate")]
        public async Task<RouteViewModel> Calculate(string fromName, string toName, string parcelType, float weight, bool recommended, FilterType filterType)
        {
            return await _routeService.CalculateRoute(fromName, toName, parcelType, weight, recommended, filterType);
        }
    }
}
