using System;
using System.ComponentModel.DataAnnotations;
using AMTDll.Models;
using Newtonsoft.Json;

namespace AMTApi.Models
{
    public class PostVehicleModel
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
        [Required]
        [JsonProperty("odometer")]
        public int Odometer { get; set; }

        public VehicleModel ToVehicleModel(){
            return new VehicleModel()
            {
                Make = this.Make,
                Model = this.Model,
                Odometer = this.Odometer,
                Plate = this.Plate,
                Year = this.Year,
                VehicleType = this.VehicleType
            };
        }
    }
}
