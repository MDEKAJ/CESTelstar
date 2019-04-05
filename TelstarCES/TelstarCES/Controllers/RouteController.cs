using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelstarCES.Data;
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
        private readonly IDataService _dataService;
        private readonly HttpClient client = new HttpClient();
        private readonly HashSet<string> _blackList;

        public RouteController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
            _blackList = ParcelTypeBlackList.BlackList;
        }

        [HttpGet]
        public async Task<ExternalRouteModel> Index(string fromName, string toName, int filter, float weight, string parcelType)
        {
            if (_blackList.Contains(parcelType))
            {
                return null;
            }

            return new ExternalRouteModel
            {
                Price = 10,
                Duration = 20,
                Valid = true
            };
        }

        [HttpGet]
        [Route("GetExternalIOA")]
        public async Task<ExternalRouteModel> GetExternalIOA(string fromName, string toName, int filter, float weight, string parcelType)
        {
            if (_blackList.Contains(parcelType))
            {
                return null;
            }

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
            if (_blackList.Contains(parcelType))
            {
                return null;
            }

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

        public string Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
