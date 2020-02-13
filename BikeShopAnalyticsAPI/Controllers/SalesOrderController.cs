using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeShopAnalytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private IRepository<SalesOrder> _salesOrderRepo;

        public SalesOrderController(IRepository<SalesOrder> salesOrderRepo)
        {
            _salesOrderRepo = salesOrderRepo;
        }


        [HttpPost("[action]")]
        public IActionResult Create(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                _salesOrderRepo.Create(salesOrder);
                return Ok("Success! Sales Order Created!");
            }
            return Problem("Error! Could not create the sales order..");
        }

        [HttpPost("[action]")]
        public IActionResult Update(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                _salesOrderRepo.Update(salesOrder);
                return Ok("Success! Sales Order Updated!");
            }
            return Problem("Error! Could not update the sales order..");
        }
    }
}
