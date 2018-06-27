using System;
using AMTDll.Models;

namespace AMTDll
{
    public interface IServicesValidation
    {
        bool Validation(MaintenanceTypeEnum maintenanceType, VehicleTypeEnum vehicleType);
    }
}
