using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class ServiceModel : VehicleServiceModel
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
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        public override string[] Validate()
        {
            var errors = new List<string>();
            if (ProviderId == Guid.Empty)
            {
                errors.Add("Invalid Provider Id");
            }
            if (VehicleId == Guid.Empty)
            {
                errors.Add("Invalid Vehicle Id");
            }
            if (Cost < 0.0)
            {
                errors.Add("Invalid Cost");
            }
            if (Odometer < 0)
            {
                errors.Add("Invalid Odometer");
            }
            var serviceVehicleValidation = new ServicesValidation().Validation(ServiceType, VehicleId);
            if (!string.IsNullOrWhiteSpace(serviceVehicleValidation))
                errors.Add(serviceVehicleValidation);
            return errors.ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj is ServiceModel veh)
                return veh.Id == Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}