using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureBeyondBasics.Controllers
{
    public class StorageController : Controller
    {
        // GET: Storage
        public ActionResult Index()
        {
            var storageConnectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("customer");
            table.CreateIfNotExists();

            var customer = new CustomerEntity(Guid.NewGuid())
            {
                FirstName = "Gulzar",
                LastName = "Hussain",
                Email = "syedg.hussain@pearson.com",
                PhoneNumber = "+61426642664"
            };

            var insertOperation = TableOperation.Insert(customer);
            table.Execute(insertOperation);
            return View(customer);
        }
    }
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(Guid customerid)
        {
            PartitionKey = "customer";
            RowKey = customerid.ToString();
        }

        public CustomerEntity() { }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}