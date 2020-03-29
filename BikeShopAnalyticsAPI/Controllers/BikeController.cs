using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeShopAnalytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController : ControllerBase
    {
        private IRepository<Bike> _bikeRepo;
        private IRepository<Auth> _authRepo;

        public BikeController(IRepository<Bike> bikeRepo, IRepository<Auth> authRepo)
        {
            _bikeRepo = bikeRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{bikeID}")]
        public async Task<ActionResult<Bike>> Read([FromHeader(Name = "Token")]string token, int bikeID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _bikeRepo.Read(b => b.BikeID == bikeID);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, Bike bike)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _bikeRepo.Create(bike);
                    return Ok("Success! Bike Created!");
                }
                return Problem("Error! Could not create the bike..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, Bike bike)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _bikeRepo.Update(bike);
                    return Ok("Success! Bike Updated!");
                }
                return Problem("Error! Could not update the Bike..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{bikeID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int bikeID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var bike = await _bikeRepo.Read(a => a.BikeID == bikeID);

                if(!(bike is null))
                {
                    await _bikeRepo.Delete(bike);
                    return Ok("Success! Bike Deleted!");
                }
                return Problem("Error! Could not delete the Bike..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Bike>>> ReadAll([FromHeader(Name = "Token")]string token)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var bikeList = await _bikeRepo.ReadAll();
                return bikeList.ToList();;
            }
            return StatusCode(403);
        }
    }
}
