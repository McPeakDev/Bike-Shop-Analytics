using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeShopAnalyticsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IRepository<Admin> _adminRepo;

        public AdminController(IRepository<Admin> adminRepo)
        {
            _adminRepo = adminRepo;
        }

        [HttpGet("[action]/{adminID}")]
        public Admin Read(int adminID)
        {
            return _adminRepo.Read(so => so.AdminID == adminID);
        }

        [HttpPost("[action]")]
        public IActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminRepo.Create(admin);
                return Ok("Success! Admin Created!");
            }
            return Problem("Error! Could not create the Admin..");
        }

        [HttpPut("[action]")]
        public IActionResult Update(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminRepo.Update(admin);
                return Ok("Success! Admin Updated!");
            }
            return Problem("Error! Could not update the admin..");
        }

        [HttpDelete("[action]/{adminID}")]
        public IActionResult Delete(int adminID)
        {
            var admin = Read(adminID);
            if (!(admin is null))
            {
                _adminRepo.Delete(admin);
                return Ok("Success! Admin Deleted!");

            }
            return Problem("Error! Could not delete the admin..");
        }

        [HttpGet("[action]")]
        public List<Admin> ReadAll()
        {
            return _adminRepo.ReadAll().ToList();
        }
    }
}