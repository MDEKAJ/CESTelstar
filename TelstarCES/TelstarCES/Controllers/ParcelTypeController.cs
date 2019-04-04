using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ParcelTypeController : ControllerBase
    {
        private readonly IDataService _dataService = new DataService();

        public async Task<ParcelType[]> ParcelTypes()
        {
            return await _dataService.GetParcelTypes();
        }

        public async Task<ParcelType> Get(int parcelTypeId)
        {
            return await _dataService.GetParcelType(parcelTypeId);
        }
    }
}