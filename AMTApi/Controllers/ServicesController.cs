using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTApi.Models;
using AMTDll;
using AMTDll.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMTApi.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController : Controller
    {
        IRepository<ServiceModel> _repo = Repository<ServiceModel>.Instance;

        [HttpGet]
        public IEnumerable<ServiceModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]
        public string Post([FromBody]PostServiceModel value)
        {
            var service = new ServiceModel()
            {
                VehicleId = value.VehicleId,
                ProviderId = value.ProviderId,
                Cost = value.Cost,
                Note = value.Note,
                Odometer = value.Odometer,
                ServiceType = value.ServiceType,
                Date = value.Date
            };
            if (service.Validate().Length == 0)
            {
                _repo.Create(service);
                return JsonConvert.SerializeObject(new ResultModel("Service Created", false));
            }
            return JsonConvert.SerializeObject(new ResultModel(service.Validate().Aggregate((a, b) => $"{a} | {b}"), true));
        }

        [HttpPut]
        public string Put([FromBody]ServiceModel value)
        {
            if (value.Validate().Length == 0)
            {
                _repo.Update(value);
                return JsonConvert.SerializeObject(new ResultModel("Service Updated", false));
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
                return JsonConvert.SerializeObject(new ResultModel("Service Deleted", false));
            }
            return JsonConvert.SerializeObject(new ResultModel("Provider Not Found", true));

        }
    }
}
