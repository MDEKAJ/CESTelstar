using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelstarCES.Data;
using TelstarCES.Data.Models;
using TelstarCES.Services;

namespace TelstarCES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ParcelTypeController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ParcelTypeController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
        }

        [HttpGet]
        [Route("ParcelTypes")]
        public async Task<ParcelType[]> ParcelTypes()
        {
            return await _dataService.GetParcelTypes();
        }

        [HttpGet]
        public async Task<ParcelType> Get(int parcelTypeId)
        {
            if (parcelTypeId < 0)
            {
                return null;
            }

            return await _dataService.GetParcelType(parcelTypeId);
        }
    }
}