using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos_DB_SignalR.DAL;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace Cosmos_DB_SignalR
{
    public static class InvoiceCollectionTriggerFunction
    {
        [FunctionName("InvoiceCollectionTrigger")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "SalesDatabase",
            collectionName: "Invoices",
            ConnectionStringSetting = "CosmosDBConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists =true)]IReadOnlyList<Document> input, 
            ILogger log,
            //SignalR output Binding
            [SignalR(HubName = "dashboard")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            if (input != null && input.Count > 0)
            {
                //Get the date from updated collection
                string date = input[0].GetPropertyValue<string>("date");
                //Calculate the sales total for the given date
                var salesTotal = new InvoiceDataService().GetDailySalesTotal(date);

                //Send sales total to SignalR method
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    Target = "updateSales",
                    Arguments = new[] { salesTotal }
                });
            }
        }
    }
}
