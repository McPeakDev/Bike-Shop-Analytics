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
    public class CRUDTestPOrderErrors
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

            PurchaseOrder pOrder = new PurchaseOrder()
            {
               PurchaseOrderID = -1
            };

            //Login as Admin
            HttpContent content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

            var auth = JsonConvert.DeserializeObject<Auth>(await result.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Add("Token", auth.Token);

            admin.AdminID = auth.AdminID;

            Assert.True(admin.Equals(auth.Admin));

            //Create PurchaseOrder
            content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/create/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Read PurchaseOrder
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/purchaseorder/read/{pOrder.PurchaseOrderID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Update PurchaseOrder
            pOrder.EmployeeID = 24; //still invalid

            content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/update/", content);

            Assert.NotEqual("OK", result.StatusCode.ToString());

            //Delete PurchaseOrder
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/purchaseorder/delete/{pOrder.PurchaseOrderID}");

            Assert.NotEqual("OK", result.StatusCode.ToString());
        }
    }
}
