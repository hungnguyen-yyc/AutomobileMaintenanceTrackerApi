using System;
using System.Collections.Generic;
using System.Linq;
using AMTDll.Models;

namespace AMTDll
{
    public class ServicesValidation : IServicesValidation
    {
        Dictionary<MaintenanceTypeEnum, VehicleTypeEnum[]> _invalidServices = new Dictionary<MaintenanceTypeEnum, VehicleTypeEnum[]>();

        public ServicesValidation(){
            _invalidServices.Add(MaintenanceTypeEnum.BatteryChange, new []{VehicleTypeEnum.Diesel, VehicleTypeEnum.Gas});
            _invalidServices.Add(MaintenanceTypeEnum.OilChange, new[] { VehicleTypeEnum.Electric });
        }

        public bool Validation(MaintenanceTypeEnum maintenanceType, VehicleTypeEnum vehicleType)
        {
            try
            {
                var vehicleTypes = _invalidServices[maintenanceType];
                if (vehicleTypes == null) return true;
                return vehicleTypes.All(v => v != vehicleType);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }
    }
}
