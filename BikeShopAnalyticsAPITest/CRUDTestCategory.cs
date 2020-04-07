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
                UserName = "CatTest"
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
                CategoryName = "Test Cat",
                XCategory= "P1",
                YCategory = "P2",
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

            var catRead = JsonConvert.DeserializeObject<Category>(await result.Content.ReadAsStringAsync());

            cat.CategoryID = catRead.CategoryID;

            //Read Category
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/category/read/{cat.CategoryID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(cat.Equals(catRead));

            //Update Category
            cat.CategoryName = "New Name";

            content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/category/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Category
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/category/delete/{cat.CategoryID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/admin/delete/{admin.AdminID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
