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
    public class CRUDTestPItem
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

            PurchaseItem pitem = new PurchaseItem()
            {
                ComponentID = 42,
                PricePaid = 42,
                Quantity = 42,
                QuantityReceived = 42
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

            //Create PurchaseItem
            content = new StringContent(JsonConvert.SerializeObject(pitem), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            var pRead = JsonConvert.DeserializeObject<PurchaseItem>(await result.Content.ReadAsStringAsync());

            pitem.PurchaseID = pRead.PurchaseID;

            //Read PurchaseItem
            result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/read/{pitem.PurchaseID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(pitem.Equals(pRead));

            //Update PurchaseItem
            pitem.ComponentID = 24;

            content = new StringContent(JsonConvert.SerializeObject(pitem), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete PurchaseItem
            result = await client.DeleteAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/delete/{pitem.PurchaseID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/admin/delete/{admin.AdminID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
