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
    public class CRUDTest
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task CRUDTestAdmin()
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


            HttpContent content = new StringContent(JsonConvert.SerializeObject(adminBundle), UnicodeEncoding.UTF8, "application/json");

            var result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            content = new StringContent(JsonConvert.SerializeObject(creds), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/admin/login/", content);

            var auth = JsonConvert.DeserializeObject<Auth>(await result.Content.ReadAsStringAsync());

            client.DefaultRequestHeaders.Add("Token", auth.Token);

            admin.AdminID = auth.AdminID;

            Assert.True(admin.Equals(auth.Admin));

            admin.MiddleName = "Changed Middle Name";

            content = new StringContent(JsonConvert.SerializeObject(admin), UnicodeEncoding.UTF8, "application/json");

            result = await client.PutAsync("https://bikeshopmonitoring.duckdns.org/api/admin/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://bikeshopmonitoring.duckdns.org/api/admin/delete/");
            request.Content = new StringContent(JsonConvert.SerializeObject(creds), Encoding.UTF8,"application/json");//CONTENT-TYPE header

            result = await client.SendAsync(request);

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
