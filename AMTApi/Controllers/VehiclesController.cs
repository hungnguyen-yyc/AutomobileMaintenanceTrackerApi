using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTApi.Models;
using AMTDll;
using AMTDll.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMTApi.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        IRepository<VehicleModel> _repo = new Repository<VehicleModel>();

        [HttpGet]
        public IEnumerable<VehicleModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]//use post vehicle to prevent id generate from client
        public void Post([FromBody]PostVehicleModel value)
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
            _repo.Create(vehicle);
        }

        [HttpPut]
        public void Put([FromBody]VehicleModel value)
        {
            _repo.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var veh = Get().FirstOrDefault(item => item.Id == new Guid(id));
            if (veh != null)
                _repo.Remove(veh);
        }
    }
}
