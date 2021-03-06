﻿using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using System.Text;
namespace BikeShopAnalyticsAPITest
{
    public class CRUDTestManTraErrors
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task CRUDTest()
        {
            AdminBundle adminBundle = new AdminBundle();

            Admin admin = new Admin()
            {
                Email = "BJB@etsu.edu",
                FirstName = "Billy",
                MiddleName = "Joe",
                LastName = "Bob",
                UserName = "BillyJB"
            };

            adminBundle.Admin = admin;
            adminBundle.Password = "asdfghij";

            Credentials creds = new Credentials()
            {
                UserName = adminBundle.Admin.UserName,
                Password = adminBundle.Password
            };

            ManufacturerTransaction manu = new ManufacturerTransaction()
            {
                ManTraID = -1
            };

            //Login as Admin
            HttpContent content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

            var auth = JsonConvert.DeserializeObject<Auth>(await result.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Add("Token", auth.Token);

            admin.AdminID = auth.AdminID;

            Assert.True(admin.Equals(auth.Admin));

            //Create manufacturertransaction
            content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/create/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Read manufacturertransaction
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/read/{manu.ManTraID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Update ManufacturerTransaction
            manu.Description = "New description";

            content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/update/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Delete ManufacturerTransaction
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/delete/{manu.ManTraID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());
        }
    }
}
