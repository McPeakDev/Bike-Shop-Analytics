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
    public class ManufacturerTransactionController : ControllerBase
    {
        private IRepository<ManufacturerTransaction> _manufacturerTransactionRepo;
        private IRepository<Auth> _authRepo;

        public ManufacturerTransactionController(IRepository<ManufacturerTransaction> manufacturerTransactionRepo, IRepository<Auth> authRepo)
        {
            _manufacturerTransactionRepo = manufacturerTransactionRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{manTraID}")]
        public async Task<ActionResult<ManufacturerTransaction>> Read([FromHeader(Name = "Token")]string token, int manTraID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _manufacturerTransactionRepo.Read(so => so.ManTraID == manTraID);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, ManufacturerTransaction manufacturerTransaction)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    var result = await _manufacturerTransactionRepo.Create(manufacturerTransaction);
                    return Ok(result);
                }
                return Problem("Error! Could not create the Manufacturer Transaction..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, ManufacturerTransaction manufacturerTransaction)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _manufacturerTransactionRepo.Update(manufacturerTransaction);
                    return Ok("Success! Manufacturer Transaction Updated!");
                }
                return Problem("Error! Could not update the Manufacturer Transaction..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{manTraID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int manTraID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var transaction = await _manufacturerTransactionRepo.Read(a => a.ManTraID == manTraID);

                if (!(transaction is null))
                {
                    await _manufacturerTransactionRepo.Delete(transaction);
                    return Ok("Success! Manufacturer Transaction Deleted!");
                }
                return Problem("Error! Could not delete the Manufacturer Transaction..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ManufacturerTransaction>>> ReadAll()
        {
            var transactionList = await _manufacturerTransactionRepo.ReadAll();
            return transactionList.ToList();
        }
    }
}
