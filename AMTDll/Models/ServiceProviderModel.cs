using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class ServiceProviderModel : EntityModel
    {
        [JsonProperty("name")]
        public string ShopName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }

        public override string[] Validate()
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(ShopName))
            {
                errors.Add("Invalid Shop Name");
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                errors.Add("Invalid Address");
            }
            if (string.IsNullOrWhiteSpace(Phone))
            {
                errors.Add("Invalid Phone");
            }
            return errors.ToArray();
        }

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