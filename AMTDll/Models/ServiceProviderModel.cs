using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class ServiceProviderModel : EntityModel
    {
        [JsonProperty("shop")]
        public string ShopName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ServiceProviderModel veh)
                return veh.Id == Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}