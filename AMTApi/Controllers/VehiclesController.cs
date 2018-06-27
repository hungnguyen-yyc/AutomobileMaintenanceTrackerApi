using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTApi.Models;
using AMTDll;
using AMTDll.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AMTApi.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        IRepository<VehicleModel> _repo = Repository<VehicleModel>.Instance;

        [HttpGet]
        public IEnumerable<VehicleModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]
        public string Post([FromBody]PostVehicleModel value)
        {
            var vehicle = new VehicleModel()
            {
                Make = value.Make,
                Model = value.Model,
                Odometer = value.Odometer,
                Plate = value.Plate,
                Year = value.Year,
                VehicleType = value.VehicleType
            };
            if (vehicle.Validate().Length == 0)
            {
                _repo.Create(vehicle);
                return JsonConvert.SerializeObject(new ResultModel("New Vehicle Created", false));
            }
            return JsonConvert.SerializeObject(new ResultModel(vehicle.Validate().Aggregate((a, b) => $"{a} | {b}"), true));
        }

        [HttpPut]
        public string Put([FromBody]VehicleModel value)
        {
            if (value.Validate().Length == 0)
            {
                _repo.Update(value);
                return JsonConvert.SerializeObject(new ResultModel($"Vehicle {value.Plate} Updated!", false));
            }
            return JsonConvert.SerializeObject(new ResultModel(value.Validate().Aggregate((a, b) => $"{a} | {b}"), true));
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var veh = Get().FirstOrDefault(item => item.Id == new Guid(id));
            if (veh != null)
            {
                _repo.Remove(veh);
                return JsonConvert.SerializeObject(new ResultModel($"Vehicle {veh.Plate} Deleted!", false));
            }
            return JsonConvert.SerializeObject(new ResultModel("Provider Not Found", true));

        }
    }
}
