﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly HttpClient client = new HttpClient();
        private const string OceanicAirlinesDomain = "https://airlines";
        private const string TradingCompanyDomain = "https://tradingcompany";

        public RouteController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
        }

        [HttpGet]
        public async Task<ExternalRouteModel> Index(string fromName, string toName, int filter, float weight, string parcelType)
        {
            return new ExternalRouteModel
            {
                Price = 10,
                Duration = 20,
                IsValid = true
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

                return JsonConvert.DeserializeObject<ExternalRouteModel>(await response.Content.ReadAsStringAsync());
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
                    $"{TradingCompanyDomain}/api/route?fromName={Uri.EscapeDataString(fromName)}&toName={Uri.EscapeDataString(toName)}&parcelType={Uri.EscapeDataString(parcelType)}&weight={weight}&filter={filter}");
                var response = await client.GetAsync(uri.Uri.AbsoluteUri);

                return JsonConvert.DeserializeObject<ExternalRouteModel>(await response.Content.ReadAsStringAsync());
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
