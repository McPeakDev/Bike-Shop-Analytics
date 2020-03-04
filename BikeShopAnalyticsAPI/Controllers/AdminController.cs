using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BikeShopAnalyticsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private IRepository<Admin> _adminRepo;
        private IRepository<Auth> _authRepo;
        private IRepository<Credentials> _credRepo;

        public AdminController(IRepository<Admin> adminRepo, IRepository<Credentials> credRepo, IRepository<Auth> authRepo)
        { 
            _adminRepo = adminRepo;
            _authRepo = authRepo;
            _credRepo = credRepo;
        }

        [HttpPost("[action]")]
        public async Task<Auth> Read(Credentials creds)
        {
            var adminCreds = await _credRepo.Read(cr => (cr.UserName == creds.UserName && cr.Password == creds.Password), cr => cr.Auth);
            var adminAuth = await _authRepo.Read(a => a.AuthID == adminCreds.AuthID, a => a.Admin);

            return adminAuth;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(AdminBundle adminBundle)
        {
            MD5 hasher = MD5.Create();

            var hashArray = hasher.ComputeHash((Encoding.UTF8.GetBytes(adminBundle.Password)));

            StringBuilder sb = new StringBuilder();

            foreach (var item in hashArray)
            {
                sb.Append(item.ToString("x2"));
            }

            Auth auth = new Auth
            {
                Admin = adminBundle.Admin,
                Token = sb.ToString()
            };

            if (ModelState.IsValid)
            {
                await _adminRepo.Create(adminBundle.Admin);
                await _credRepo.Create(new Credentials
                {
                    UserName = adminBundle.Admin.UserName,
                    Password = adminBundle.Password,
                    Auth = auth
                });
                return Ok("Success! Admin Created!");
            }
            return Problem("Error! Could not create the admin..");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(Admin admin)
        {
            if (ModelState.IsValid)
            {
                await _adminRepo.Update(admin);
                return Ok("Success! Admin Updated!");
            }
            return Problem("Error! Could not update the admin..");
        }

        [HttpDelete("[action]/{adminID}")]
        public async Task<IActionResult> Delete(int adminID)
        {
            var admin = new Admin();//Read(adminID);
            if (!(admin is null))
            {
                await _adminRepo.Delete(admin);
                return Ok("Success! Admin Deleted!");

            }
            return Problem("Error! Could not delete the admin..");
        }

        [HttpGet("[action]")]
        public async Task<List<Admin>> ReadAll()
        {
            var adminList = await _adminRepo.ReadAll();
            return adminList.ToList();
        }
    }
}