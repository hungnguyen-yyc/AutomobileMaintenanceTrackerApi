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

        public ServiceModel ToServiceModel(){
            return new ServiceModel { 
                ProviderId = this.ProviderId,
                ServiceType = this.ServiceType,
                VehicleId = this.VehicleId,
                Note = this.Note,
                Cost = this.Cost,
                Odometer = this.Odometer,
                Date = this.Date
            };
        }
    }
}
