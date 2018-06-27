using System;
using System.ComponentModel.DataAnnotations;
using AMTDll.Models;
using Newtonsoft.Json;

namespace AMTApi.Models
{
    public class PostServiceProviderModel
    {
        [JsonProperty("name")]
        public string ShopName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }

        public ServiceProviderModel ToServiceProviderModel(){
            return new ServiceProviderModel()
            {
                ShopName = this.ShopName,
                Address = this.Address,
                Phone = this.Phone
            };
        }
    }
}
