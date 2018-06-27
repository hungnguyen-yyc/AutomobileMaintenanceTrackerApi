using System;
using Newtonsoft.Json;

namespace AMTDll.Models
{
    public class EntityModel
    {
        public EntityModel()
        {
            Id = Guid.NewGuid();
            LastUpdated = DateTime.Now;
        }
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }
        public virtual string[] Validate()
        {
            return new[] {""};
        }
    }
}