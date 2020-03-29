using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BikeShopAnalytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private IRepository<SalesOrder> _salesOrderRepo;
        private IRepository<Auth> _authRepo;

        public SalesOrderController(IRepository<SalesOrder> salesOrderRepo, IRepository<Auth> authRepo)
        {
            _salesOrderRepo = salesOrderRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{salesID}")]
        public async Task<ActionResult<SalesOrder>> Read([FromHeader(Name = "Token")]string token, int salesID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _salesOrderRepo.Read(so => so.SalesID == salesID, s => s.Bike);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, SalesOrder salesOrder)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _salesOrderRepo.Create(salesOrder);
                    return Ok("Success! Sales Order Created!");
                }
                return Problem("Error! Could not create the sales order..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, SalesOrder salesOrder)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _salesOrderRepo.Update(salesOrder);
                    return Ok("Success! Sales Order Updated!");
                }
                return Problem("Error! Could not update the sales order..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{salesID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int salesID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var sales = await _salesOrderRepo.Read(a => a.SalesID == salesID);

                if (!(sales is null))
                {
                    await _salesOrderRepo.Delete(sales);
                    return Ok("Success! Sales Order Deleted!");
                }
                return Problem("Error! Could not delete the sales order..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<SalesOrder>>> ReadAll([FromHeader(Name = "Token")]string token)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var salesList = await _salesOrderRepo.ReadAll(s => s.Bike);
                return salesList.ToList();
            }
            return StatusCode(403);
        }
    }
}
