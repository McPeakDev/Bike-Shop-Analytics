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
    public class CRUDTestCategoryErrors
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

            Category cat = new Category()
            {
                CategoryID = -1
            };

            //Login as Admin
            HttpContent content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

            var auth = JsonConvert.DeserializeObject<Auth>(await result.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Add("Token", auth.Token);

            admin.AdminID = auth.AdminID;

            Assert.True(admin.Equals(auth.Admin));

            //Create Category
            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/create/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Read Category
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/category/read/{cat.CategoryID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Update Category
            cat.CategoryName = "New Name";

            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/category/update/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Delete Category
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/category/delete/{cat.CategoryID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());
        }
    }
}
