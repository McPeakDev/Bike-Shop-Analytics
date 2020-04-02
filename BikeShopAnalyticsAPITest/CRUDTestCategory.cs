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
    public class CRUDTestCategory
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

            Category cat = new Category()
            {
                CategoryID = 99999,
                CategoryName = "Test Cat",
                PlotItemOne = "P1",
                PlotItemTwo = "P2",
                ChartType = "pie",
                StartRange = new DateTime(),
                EndRange = new DateTime()
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

            //Create Category
            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Read Category
            result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/Category/read/99999");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.Equal(result.Content.ToString(), cat.ToString());

            //Update Category
            cat.CategoryName = "New Name";

            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Category
            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/delete/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://bikeshopmonitoring.duckdns.org/api/admin/delete/");
            request.Content = new StringContent(JsonConvert.SerializeObject(creds), Encoding.UTF8, "application/json");//CONTENT-TYPE header

            result = await client.SendAsync(request);

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
