using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using System.Text;
namespace BikeShopAnalyticsAPITest
{
    public class CRUDTestSalesOrder
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

            SalesOrder sales = new SalesOrder()
            {
                BikeID = 1,
                ListPrice = 42,
                SalePrice = 42,
                Tax = 42,
                Discount = 42,
                OrderDate = new DateTime(),
                StartDate = new DateTime(),
                ShipDate = new DateTime(),
                StoreID = 42,
                State = "TN"
            };

            Bike bike = new Bike()
            {
                Name = "Test Bike",
                SalesPrice = 42
            };

            //Login as Admin
            HttpContent content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

            var auth = JsonConvert.DeserializeObject<Auth>(await result.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Add("Token", auth.Token);

            admin.AdminID = auth.AdminID;

            Assert.True(admin.Equals(auth.Admin));

            //Create Bike
            content = new StringContent(JsonConvert.SerializeObject(bike), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/bike/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            var bikeRead = JsonConvert.DeserializeObject<Bike>(await result.Content.ReadAsStringAsync());

            bike.BikeID = bikeRead.BikeID;

            sales.BikeID = bike.BikeID;

            //Create SalesOrder
            content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            var saleRead = JsonConvert.DeserializeObject<SalesOrder>(await result.Content.ReadAsStringAsync());

            sales.SalesID = saleRead.SalesID;

            //Read SalesOrder
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/salesorder/read/{sales.SalesID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(sales.Equals(saleRead));

            //Update SalesOrder
            sales.ListPrice = 24;

            content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete SalesOrder
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/salesorder/delete/{sales.SalesID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Bike
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/bike/delete/{bike.BikeID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
