using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class VehicleModel : VehicleServiceModel
    {
        [Required]
        [JsonProperty("make")]
        public string Make { get; set; }
        [Required]
        [JsonProperty("model")]
        public string Model { get; set; }
        [Required]
        [JsonProperty("plate")]
        public string Plate { get; set; }
        [Required] 
        [JsonProperty("year")]
        public int Year { get; set; }
        [Required]
        [JsonProperty("type")]
        public VehicleTypeEnum VehicleType { get; set; }

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
