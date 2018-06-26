using System;

namespace AMTDll.Models
{
    public class ServiceModel : VehicleServiceModel
    {
        public ServiceModel() : base() {}
        public MaintenanceTypeEnum ServiceType { get; set; }
        public double Cost { get; set; }
    }
}