using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

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

        [HttpGet("[action]/{salesID}")]
        public async Task<SalesOrder> Read(int salesID)
        {
            return await _salesOrderRepo.Read(so => so.SalesID == salesID);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                await _salesOrderRepo.Create(salesOrder);
                return Ok("Success! Sales Order Created!");
            }
            return Problem("Error! Could not create the sales order..");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                await _salesOrderRepo.Update(salesOrder);
                return Ok("Success! Sales Order Updated!");
            }
            return Problem("Error! Could not update the sales order..");
        }

        [HttpDelete("[action]/{salesID}")]
        public async Task<IActionResult> Delete(int salesID)
        {
            var sales= new SalesOrder();//Read(salesID);
            if (!(sales is null))
            {
                await _salesOrderRepo.Delete(sales);
                return Ok("Success! Sales Order Deleted!");

            }
            return Problem("Error! Could not delete the sales order..");
        }

        [HttpGet("[action]")]
        public async Task<List<SalesOrder>> ReadAll()
        {
            var salesList = await _salesOrderRepo.ReadAll().ToList();
            return salesList;
        }
    }
}
