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
    public class CRUDTestManTra
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
                UserName = "ManTest"
            };

            adminBundle.Admin = admin;
            adminBundle.Password = "123456789!";

            Credentials creds = new Credentials()
            {
                UserName = adminBundle.Admin.UserName,
                Password = adminBundle.Password
            };

            ManufacturerTransaction manu = new ManufacturerTransaction()
            {
                TransactionDate = new DateTime(),
                EMPLOYEEID = 42,
                Amount = 42,
                Description = "Test Manu",
                Reference = 42
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

            //Create manufacturertransaction
            content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            var manTraRead = JsonConvert.DeserializeObject<ManufacturerTransaction>(await result.Content.ReadAsStringAsync());

            manu.ManTraID = manTraRead.ManTraID;

            //Read manufacturertransaction
            result = await client.GetAsync($"https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/read/{manu.ManTraID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(manu.Equals(manTraRead));

            //Update ManufacturerTransaction
            manu.Description = "New description";

            content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete ManufacturerTransaction
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/delete/{manu.ManTraID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/admin/delete/{admin.AdminID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
