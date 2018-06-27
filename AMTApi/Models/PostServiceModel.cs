using System;
using AMTDll.Models;
using Newtonsoft.Json;

namespace AMTApi.Models
{
    public class PostServiceModel
    {
        [JsonProperty("provider")]
        public Guid ProviderId { get; set; }
        [JsonProperty("type")]
        public MaintenanceTypeEnum ServiceType { get; set; }
        [JsonProperty("vehicle")]
        public Guid VehicleId { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("cost")]
        public double Cost { get; set; }
        [JsonProperty("odometer")]
        public int Odometer { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
