using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OrderController : ControllerBase
    {
        private readonly IDataService _dataService;

        public OrderController(ApplicationDbContext db)
        {
            _dataService = new DataService(db);
        }

        [HttpPost]
        public async Task<bool> Post(Order order)
        {
            if (string.IsNullOrWhiteSpace(order?.FromCity) || string.IsNullOrWhiteSpace(order.ToCity))
            {
                return false;
            }

            if (order.Weight < 0 || order.TotalDuration < 0 || order.TotalPrice < 0)
            {
                return false;
            }

            if (order.Segments == null || order.Customer == null || order.ParcelType == null)
            {
                return false;
            }

            return await _dataService.AddOrder(order);
        }
    }
}