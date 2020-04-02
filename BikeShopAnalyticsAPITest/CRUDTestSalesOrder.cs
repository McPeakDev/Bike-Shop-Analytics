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

            SalesOrder sales = new SalesOrder()
            {
                SalesID = 99999,
                BikeID = 42,
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

            //Create SalesOrder
            content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Read SalesOrder
            result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/read/99999");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.Equal(result.Content.ToString(), sales.ToString());

            //Update SalesOrder
            sales.ListPrice = 24;

            content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete SalesOrder
            content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/delete/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://bikeshopmonitoring.duckdns.org/api/admin/delete/");
            request.Content = new StringContent(JsonConvert.SerializeObject(creds), Encoding.UTF8, "application/json");//CONTENT-TYPE header

            result = await client.SendAsync(request);

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
