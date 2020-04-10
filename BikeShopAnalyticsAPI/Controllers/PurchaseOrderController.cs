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
    public class PurchaseOrderController : ControllerBase
    {
        private IRepository<PurchaseOrder> _purchaseOrderRepo;
        private IRepository<Auth> _authRepo;

        public PurchaseOrderController(IRepository<PurchaseOrder> purchaseOrderRepo, IRepository<Auth> authRepo)
        {
            _purchaseOrderRepo = purchaseOrderRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{purchaseID}")]
        public async Task<ActionResult<PurchaseOrder>> Read([FromHeader(Name = "Token")]string token, int purchaseID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _purchaseOrderRepo.Read(so => so.PurchaseOrderID == purchaseID);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, PurchaseOrder purchaseOrder)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    var result = await _purchaseOrderRepo.Create(purchaseOrder);
                    return Ok(result);
                }
                return Problem("Error! Could not create the Purchase Order..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, PurchaseOrder purchaseOrder)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _purchaseOrderRepo.Update(purchaseOrder);
                    return Ok("Success! Purchase Order Updated!");
                }
                return Problem("Error! Could not update the Purchase Order..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{purchaseID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int purchaseID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var purchaseOrder = await _purchaseOrderRepo.Read(a => a.PurchaseOrderID == purchaseID);

                if (!(purchaseOrder is null))
                {
                    await _purchaseOrderRepo.Delete(purchaseOrder);
                    return Ok("Success! Purchase Order Deleted!");
                }
                return Problem("Error! Could not delete the Purchase Order..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<PurchaseOrder>>> ReadAll()
        {
            var purchaseOrderList = await _purchaseOrderRepo.ReadAll();
            return purchaseOrderList.ToList();
        }
    }
}
