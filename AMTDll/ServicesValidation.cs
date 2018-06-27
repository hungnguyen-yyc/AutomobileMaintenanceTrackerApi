using System;
using System.Collections.Generic;
using System.Linq;
using AMTDll.Models;

namespace AMTDll
{
    public class ServicesValidation
    {
        Dictionary<MaintenanceTypeEnum, VehicleTypeEnum[]> _invalidServices = new Dictionary<MaintenanceTypeEnum, VehicleTypeEnum[]>();

        public ServicesValidation(){
            _invalidServices.Add(MaintenanceTypeEnum.BatteryChange, new []{VehicleTypeEnum.Diesel, VehicleTypeEnum.Gas});
            _invalidServices.Add(MaintenanceTypeEnum.OilChange, new[] { VehicleTypeEnum.Electric });
        }

        bool Validation(MaintenanceTypeEnum maintenanceType, VehicleTypeEnum vehicle)
        {
            try
            {
                var vehicleTypes = _invalidServices[maintenanceType];
                if (vehicleTypes == null) return true;
                return vehicleTypes.All(v => v != vehicle);
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

        public string Validation(MaintenanceTypeEnum maintenanceType, Guid VehicleId)
        {
            try
            {
                var vehicles = Repository<VehicleModel>.Instance.Read();
                if (vehicles == null) return "Vehicles Not Available";
                var vehicle = vehicles.FirstOrDefault(v => v.Id == VehicleId);
                if (vehicle == null) return "Vehicle Not Found";
                if (Validation(maintenanceType, vehicle.VehicleType))
                    return "";
                return $"{maintenanceType.ToString()} isn't provided to this type ({vehicle.VehicleType.ToString()}) of vehicle";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
