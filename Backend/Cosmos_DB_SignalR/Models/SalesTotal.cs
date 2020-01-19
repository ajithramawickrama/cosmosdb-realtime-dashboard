using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos_DB_SignalR.Models
{
    public class SalesTotal
    {
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
