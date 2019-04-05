using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelstarCES.Constants;
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
        private const string OceanicAirlinesDomain = "http://wa-oadk.azurewebsites.net";
        private const string TradingCompanyDomain = "http://wa-eitdk.azurewebsites.net";
        private readonly HttpClient client = new HttpClient();
        private readonly IRouteService _routeService;
        private readonly IDataService _dataService;
        private readonly HashSet<string> _blackList;

        public RouteController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
            _routeService = new RouteService(_dataService, this);
            _blackList = ParcelTypeBlackList.BlackList;
        }

        [HttpGet]
        public async Task<ExternalRouteModel> Index(string fromName, string toName, int filter, float weight, string parcelType)
        {
            if (weight >= RouteMetrics.MaxWeight || _blackList.Contains(parcelType))
            {
                return new ExternalRouteModel
                {
                    Duration = 0,
                    Price = 0,
                    Valid = false
                };
            }

            var path = await _routeService.CalculateRoute(fromName, toName, parcelType, weight, false, (FilterType) filter);            
            return new ExternalRouteModel
            {
                Price = (decimal)(path?.TotalPrice ?? 0),
                Duration = path?.TotalDuration ?? 0,
                Valid = path?.Segments?.Length > 0
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
        public async Task<RouteViewModel> Calculate(string fromName, string toName, string parcelType, float weight, bool recommended, int filterType)
        {
            if (weight >= RouteMetrics.MaxWeight || _blackList.Contains(parcelType))
            {
                return new RouteViewModel
                {
                    TotalPrice = 0f,
                    TotalDuration = 0,
                    Segments = new Segment[0]
                };
            }

            return await _routeService.CalculateRoute(fromName, toName, parcelType, weight, recommended, (FilterType)filterType);
        }
    }
}
