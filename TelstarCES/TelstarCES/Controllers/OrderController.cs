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
        public async Task<int> Post(Order order)
        {
            if (string.IsNullOrWhiteSpace(order?.FromCity) || string.IsNullOrWhiteSpace(order.ToCity))
            {
                return -1;
            }

            if (order.Weight < 0 || order.TotalDuration < 0 || order.TotalPrice < 0)
            {
                return -1;
            }

            if (order.Segments == null || order.Customer == null || order.ParcelType == null)
            {
                return -1;
            }

            return await _dataService.AddOrder(order);
        }
    }
}
