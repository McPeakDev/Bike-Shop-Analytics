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
    [Route("[controller]")]
    public class PurchaseItemController : ControllerBase
    {
        private IRepository<PurchaseItem> _purchaseItemRepo;
        private IRepository<Auth> _authRepo;

        public PurchaseItemController(IRepository<PurchaseItem> purchaseItemRepo, IRepository<Auth> authRepo)
        {
            _purchaseItemRepo = purchaseItemRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{purchaseID}")]
        public async Task<ActionResult<PurchaseItem>> Read([FromHeader(Name = "Token")]string token, int purchaseID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _purchaseItemRepo.Read(so => so.PurchaseID == purchaseID);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, PurchaseItem purchaseItem)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    var result = await _purchaseItemRepo.Create(purchaseItem);
                    return Ok(result);
                }
                return Problem("Error! Could not create the Purchase Item..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, PurchaseItem purchaseItem)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _purchaseItemRepo.Update(purchaseItem);
                    return Ok("Success! Purchase Item Updated!");
                }
                return Problem("Error! Could not update the Purchase Item..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{purchaseID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int purchaseID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var purchaseItem = await _purchaseItemRepo.Read(a => a.PurchaseID == purchaseID);

                if (!(purchaseItem is null))
                {
                    await _purchaseItemRepo.Delete(purchaseItem);
                    return Ok("Success! Purchase Item Deleted!");
                }
                return Problem("Error! Could not delete the Purchase Item..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<PurchaseItem>>> ReadAll([FromHeader(Name = "Token")]string token)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var purchaseItemList = await _purchaseItemRepo.ReadAll();
                return purchaseItemList.ToList();
            }
            return StatusCode(403);
        }
    }
}
