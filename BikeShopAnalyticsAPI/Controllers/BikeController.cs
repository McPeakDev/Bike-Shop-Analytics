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
    public class BikeController : ControllerBase
    {
        private IRepository<Bike> _bikeRepo;

        public BikeController(IRepository<Bike> bikeRepo)
        {
            _bikeRepo = bikeRepo;
        }


        [HttpPost("[action]")]
        public IActionResult Create(Bike bike)
        {
            if (ModelState.IsValid)
            {
                _bikeRepo.Create(bike);
                return Ok("Success! Bike Created!");
            }
            return Problem("Error! Could not create the bike..");
        }

        [HttpPost("[action]")]
        public IActionResult Update(Bike bike)
        {
            if (ModelState.IsValid)
            {
                _bikeRepo.Update(bike);
                return Ok("Success! Bike Updated!");
            }
            return Problem("Error! Could not update the Bike..");
        }
    }
}
