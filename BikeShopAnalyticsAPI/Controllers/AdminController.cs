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
using Newtonsoft.Json;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

namespace BikeShopAnalyticsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        #region Properties
        private IRepository<Admin> _adminRepo;
        private IRepository<Auth> _authRepo;
        private IRepository<Credentials> _credRepo;
        #endregion

        #region Constructor
        public AdminController(IRepository<Admin> adminRepo, IRepository<Credentials> credRepo, IRepository<Auth> authRepo)
        {
            _adminRepo = adminRepo;
            _authRepo = authRepo;
            _credRepo = credRepo;
        }
       
        #endregion
        
        [HttpPost("[action]")]
        public async Task<ActionResult<Auth>> Login(Credentials creds)
        {
            var adminCreds = await _credRepo.Read(cr => (cr.UserName == creds.UserName && cr.Password == creds.Password), cr => cr.Auth);
            var adminAuth = await _authRepo.Read(a => a.AuthID == adminCreds.AuthID, a => a.Admin);

            if(!(adminAuth is null))
            {
                return adminAuth;
            }
            return Problem("Error! The User and Password does not match");

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(AdminBundle adminBundle)
        {
            MD5 hasher = MD5.Create();

            var hashArray = hasher.ComputeHash((Encoding.UTF8.GetBytes($"{adminBundle.Password}{adminBundle.Admin.UserName}")));

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
                try
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
                catch
                {
                    return Problem("Error! That username already exists..");
                }
            }
            return Problem("Error! Could not create the admin..");

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, Admin admin)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _adminRepo.Update(admin);
                    return Ok("Success! Admin Updated!");
                }
                return Problem("Error! Could not update the admin..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{adminID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int adminID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var adminAuth = await _authRepo.Read(a => a.Token == token, a => a.Admin);
                var adminDelete = await _adminRepo.Read(a => a.AdminID == adminID);

                if (!(adminAuth is null))
                {
                    await _adminRepo.Delete(adminDelete);
                    return Ok("Success! Admin Deleted!");
                }
                return Problem("Error! Could not delete the admin..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Admin>>> ReadAll()
        {
            var adminList = await _adminRepo.ReadAll();
            return adminList.ToList();
        }

      
    }
}