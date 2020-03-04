﻿using System;
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
        public Auth Read(Credentials creds)
        {
            var adminCreds = _credRepo.Read(cr => (cr.UserName == creds.UserName && cr.Password == creds.Password), cr => cr.Auth);
            var adminAuth = _authRepo.Read(a => a.AuthID == adminCreds.AuthID, a => a.Admin);

            return adminAuth;
        }

        [HttpPost("[action]")]
        public IActionResult Create(AdminBundle adminBundle)
        {
            SHA512 sha = new SHA512Managed();
            Auth auth = new Auth
            {
                Admin = adminBundle.Admin,
                Token = Encoding.Default.GetString(sha.ComputeHash(Encoding.Default.GetBytes(adminBundle.Password)))
            };

            if (ModelState.IsValid)
            {
                _adminRepo.Create(adminBundle.Admin);
                _credRepo.Create(new Credentials
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
            var admin = new Admin();//Read(adminID);
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