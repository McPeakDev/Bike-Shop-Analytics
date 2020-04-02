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
    public class CRUDTestBike
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task CRUDTest()
        {
            AdminBundle adminBundle = new AdminBundle();

            Admin admin = new Admin()
            {
                Email = "test@test.com",
                FirstName = "Unit",
                MiddleName = "Test",
                LastName = "Code",
                UserName = "Test"
            };

            adminBundle.Admin = admin;
            adminBundle.Password = "123456789!";

            Credentials creds = new Credentials()
            {
                UserName = adminBundle.Admin.UserName,
                Password = adminBundle.Password
            };

            Bike bike = new Bike()
            {
                Name = "Test Bike",
                SalesPrice = 42
            };

            //Create Admin
            HttpContent content = new StringContent(JsonConvert.SerializeObject(adminBundle), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Login as Admin
            content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

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

            //Read Bike
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/bike/read/{bike.BikeID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(bike.Equals(bikeRead));

            //Update Bike
            bike.Name = "New Name";

            content = new StringContent(JsonConvert.SerializeObject(bike), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/bike/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Bike
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/bike/delete/{bike.BikeID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Admin
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/admin/delete/{admin.AdminID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
