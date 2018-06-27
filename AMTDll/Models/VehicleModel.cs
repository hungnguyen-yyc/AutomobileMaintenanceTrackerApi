using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class VehicleModel : VehicleServiceModel
    {

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("type")]
        public VehicleTypeEnum VehicleType { get; set; }

        public override string[] Validate()
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(Make))
            {
                errors.Add("Invalid Shop Name");
            }
            if (string.IsNullOrWhiteSpace(Model))
            {
                errors.Add("Invalid Address");
            }
            if (string.IsNullOrWhiteSpace(Plate))
            {
                errors.Add("Invalid Phone");
            }
            if (Year < 0)
            {
                errors.Add("Invalid Year");
            }
            if (Odometer < 0)
            {
                errors.Add("Invalid Year");
            }
            return errors.ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj is VehicleModel veh)
                return veh.Id == Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
