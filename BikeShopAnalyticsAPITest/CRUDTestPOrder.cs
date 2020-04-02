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
    public class CRUDTestPOrder
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
                UserName = "OrderTest"
            };

            adminBundle.Admin = admin;
            adminBundle.Password = "123456789!";

            Credentials creds = new Credentials()
            {
                UserName = adminBundle.Admin.UserName,
                Password = adminBundle.Password
            };

            PurchaseOrder pOrder = new PurchaseOrder()
            {
                EmployeeID = 42,
                ManuID = 42,
                TotalList = 42,
                ShippingCost = 42,
                Discount = 42,
                OrderDate = new DateTime(),
                ReceiveDate = new DateTime(),
                AmountDue = 42
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

            //Create PurchaseOrder
            content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/create/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            var poRead = JsonConvert.DeserializeObject<PurchaseOrder>(await result.Content.ReadAsStringAsync());

            pOrder.PurchaseID = poRead.PurchaseID;

            //Read PurchaseOrder
            result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/read/{pOrder.PurchaseID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            Assert.True(pOrder.Equals(poRead));

            //Update PurchaseOrder
            pOrder.EmployeeID = 24;

            content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/update/", content);

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete PurchaseOrder
            result = await client.DeleteAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/delete/{pOrder.PurchaseID}");

            Assert.Equal("OK", result.StatusCode.ToString());

            //Delete Test Admin before finish
            result = await client.DeleteAsync($"https://bikeshopmonitoring.duckdns.org/api/admin/delete/{admin.AdminID}");

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
