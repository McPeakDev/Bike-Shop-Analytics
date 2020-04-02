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
    public class CRUDTestAdmin
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
                BikeID = 99999,
                Name = "Test Bike",
                SalesPrice = 42
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

            ManufacturerTransaction manu = new ManufacturerTransaction()
            {
                ManTraID = 99999,
                TransactionDate = new DateTime(),
                EMPLOYEEID = 42,
                Amount = 42,
                Description = "Test Manu",
                Reference = 42
            };

            PurchaseItem pitem = new PurchaseItem()
            {
                PurchaseID = 99999,
                ComponentID = 42,
                PricePaid = 42,
                Quantity = 42,
                QuantityReceived = 42
            };

            PurchaseOrder pOrder = new PurchaseOrder()
            {
                PurchaseID = 99999,
                EmployeeID = 42,
                ManuID = 42,
                TotalList = 42,
                ShippingCost = 42,
                Discount = 42,
                OrderDate = new DateTime(),
                ReceiveDate = new DateTime(),
                AmountDue = 42
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

            //Create Bike
            //content = new StringContent(JsonConvert.SerializeObject(bike), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/bike/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read Bike
            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/bike/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), bike.ToString());

            ////Update Bike
            //bike.Name = "New Name";

            //content = new StringContent(JsonConvert.SerializeObject(bike), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/bike/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete Bike
            //content = new StringContent(JsonConvert.SerializeObject(bike), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/bike/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Create Category
            //content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read Category
            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/Category/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), cat.ToString());

            ////Update Category
            //cat.CategoryName = "New Name";

            //content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete Category
            //content = new StringContent(JsonConvert.SerializeObject(cat), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/category/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Create manufacturertransaction
            //content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read manufacturertransaction
            ////content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), manu.ToString());

            ////Update ManufacturerTransaction
            //manu.Description = "New description";

            //content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete ManufacturerTransaction
            //content = new StringContent(JsonConvert.SerializeObject(manu), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/manufacturertransaction/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Create PurchaseItem
            //content = new StringContent(JsonConvert.SerializeObject(pitem), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read PurchaseItem
            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), pitem.ToString());

            ////Update PurchaseItem
            //pitem.ComponentID = 24;

            //content = new StringContent(JsonConvert.SerializeObject(pitem), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete PurchaseItem
            //content = new StringContent(JsonConvert.SerializeObject(pitem), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseitem/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Create PurchaseOrder
            //content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read PurchaseOrder
            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), pOrder.ToString());

            ////Update PurchaseOrder
            //pOrder.EmployeeID = 24;

            //content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete PurchaseOrder
            //content = new StringContent(JsonConvert.SerializeObject(pOrder), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/purchaseorder/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Create SalesOrder
            //content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/create/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Read SalesOrder
            //result = await client.GetAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/read/99999");

            //Assert.Equal("OK", result.StatusCode.ToString());

            //Assert.Equal(result.Content.ToString(), sales.ToString());

            ////Update SalesOrder
            //sales.ListPrice = 24;

            //content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/update/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            ////Delete SalesOrder
            //content = new StringContent(JsonConvert.SerializeObject(sales), UnicodeEncoding.UTF8, "application/json");

            //result = await client.PostAsync("https://bikeshopmonitoring.duckdns.org/api/salesorder/delete/", content);

            //Assert.Equal("OK", result.StatusCode.ToString());

            //delete admin
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://bikeshopmonitoring.duckdns.org/api/admin/delete/");
            request.Content = new StringContent(JsonConvert.SerializeObject(creds), Encoding.UTF8,"application/json");//CONTENT-TYPE header

            result = await client.SendAsync(request);

            Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}
