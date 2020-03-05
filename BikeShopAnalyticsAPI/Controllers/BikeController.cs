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
    [Route("[controller]")]
    public class BikeController : ControllerBase
    {
        private IRepository<Bike> _bikeRepo;

        public BikeController(IRepository<Bike> bikeRepo)
        {
            _bikeRepo = bikeRepo;
        }

        [HttpGet("[action]/{bikeID}")]
        public async Task<Bike> Read(int bikeID)
        {
            return await _bikeRepo.Read(b => b.BikeID == bikeID);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(Bike bike)
        {
            if (ModelState.IsValid)
            {
                await _bikeRepo.Create(bike);
                return Ok("Success! Bike Created!");
            }
            return Problem("Error! Could not create the bike..");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(Bike bike)
        {
            if (ModelState.IsValid)
            {
                await _bikeRepo.Update(bike);
                return Ok("Success! Bike Updated!");
            }
            return Problem("Error! Could not update the Bike..");
        }

        [HttpDelete("[action]/{bikeID}")]
        public async Task<IActionResult> Delete(int bikeID)
        {
            var bike = new Bike();//Read(bikeID);
            if(!(bike is null))
            {
                await _bikeRepo.Delete(bike);
                return Ok("Success! Bike Deleted!");
            }
            return Problem("Error! Could not delete the Bike..");
        }

        [HttpGet("[action]")]
        public async Task<List<Bike>> ReadAll()
        {
            var bikeList = await _bikeRepo.ReadAll().ToList();
            return bikeList;
        }
    }
}
