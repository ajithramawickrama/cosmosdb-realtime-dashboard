using Cosmos_DB_SignalR.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;

using System.Collections.Generic;
using System.Text;

namespace Cosmos_DB_SignalR.DAL
{
    public class InvoiceDataService
    {
        private static readonly string DatabaseID = "SalesDatabase"; //Name of the Cosmos Db databasae
        private static readonly string CollectionId = "Invoices"; // Name of the container

        public SalesTotal GetDailySalesTotal(string date)
        {
            var serviceEndPoint = new Uri(Environment.GetEnvironmentVariable("DatabaseEndpoint"));
            DocumentClient client = new DocumentClient(serviceEndPoint, Environment.GetEnvironmentVariable("DatabaseAccountKey"));

            var docuementCollectionLink = UriFactory.CreateDocumentCollectionUri(DatabaseID, CollectionId);
            SalesTotal salesTotal = new SalesTotal();
            try
            {
                var query = string.Format("SELECT * FROM Invoices p where p.date='{0}'", date);
                var queryResult = client.CreateDocumentQuery<Invoice>(docuementCollectionLink, query, new FeedOptions { EnableCrossPartitionQuery = true })
                    .ToList().SelectMany(t => t.InvoiceDetails).Sum(t => t.LineTotal);
                salesTotal.Total = queryResult;
            }
            catch (Exception ex)
            {
                return salesTotal;
            }
           

            return salesTotal;
        }


    }
}
