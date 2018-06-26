using System;

namespace AMTDll.Models
{
    public class ServiceHistoryModel : EntityModel
    {
        public Guid VehicleId { get; set; }
        public Guid ServiceId { get; set; }
        public string Note { get; set; }
    }
}