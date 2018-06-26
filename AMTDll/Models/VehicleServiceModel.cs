using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class VehicleServiceModel : EntityModel
    {
        public VehicleServiceModel() : base(){}
        [JsonProperty("odometer")]
        public int Odometer { get; set; }
    }
}
