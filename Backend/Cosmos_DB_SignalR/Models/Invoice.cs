using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos_DB_SignalR.Models
{
    public class Invoice
    {
        public string Id { get; set; }

        public string InvoiceNo { get; set; }

        public string Date { get; set; }

        public string UserId { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; }

    }

    public class InvoiceDetail
    {
        public string ItemName { get; set; }

        public int Qty { get; set; }

        public int UnitPrice { get; set; }

        public int LineTotal { get; set; } 
    }
}
