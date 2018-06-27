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
        IRepository<ServiceModel> _repo;
        IRepository<VehicleModel> _vehicleRepo;
        IServicesValidation _serviceValidation;

        public ServicesController(IRepository<ServiceModel> repository, IServicesValidation servicesValidation, IRepository<VehicleModel> vehicleRepo){
            _repo = repository;
            _vehicleRepo = vehicleRepo;
            _serviceValidation = servicesValidation;
        }

        [HttpGet]
        public IEnumerable<ServiceModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]
        public string Post([FromBody]PostServiceModel value)
        {
            try{
                var service = value.ToServiceModel();
                var validation = ValidateService(service);
                if (validation.Length == 0)
                {
                    _repo.Create(service);
                    return JsonConvert.SerializeObject(new ResultModel("Service Created", false));
                }
                return JsonConvert.SerializeObject(new ResultModel(validation.Aggregate((a, b) => $"{a} | {b}"), true));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel(ex.Message, true));
            }
        }

        [HttpPut]
        public string Put([FromBody]ServiceModel value)
        {
            try
            {
                var validation = ValidateService(value);
                if (validation.Length == 0)
                {
                    _repo.Update(value);
                    return JsonConvert.SerializeObject(new ResultModel("Service Updated", false));
                }
                return JsonConvert.SerializeObject(new ResultModel(validation.Aggregate((a, b) => $"{a} | {b}"), true));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel(ex.Message, true));
            }

        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            try
            {
                var veh = Get().FirstOrDefault(item => item.Id == new Guid(id));
                if (veh != null)
                {
                    _repo.Remove(veh);
                    return JsonConvert.SerializeObject(new ResultModel("Service Deleted", false));
                }
                return JsonConvert.SerializeObject(new ResultModel("Provider Not Found", true));

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel(ex.Message, true));
            }

        }

        string[] ValidateService(ServiceModel service){

            var errors = new List<string>();
            try
            {
                errors.AddRange(service.Validate());

                var vehicles = _vehicleRepo.Read();
                if (vehicles == null)
                {
                    errors.Add("Vehicles Not Available");
                    return errors.ToArray();
                }
                var vehicle = vehicles.FirstOrDefault(v => v.Id == service.VehicleId);
                if (vehicle == null)
                {
                    errors.Add("Vehicle Not Found");
                    return errors.ToArray();
                }
                if (vehicle.Odometer > service.Odometer){
                    errors.Add($"Vehicle {vehicle.Make} {vehicle.Model} {vehicle.Plate} odometer should be smaller than service odometer");
                }
                if (_serviceValidation.Validation(service.ServiceType, vehicle.VehicleType))
                    return errors.ToArray();
                errors.Add($"{service.ServiceType.ToString()} isn't provided to this type ({vehicle.VehicleType.ToString()}) of vehicle");
            }
            catch(Exception ex)
            {
                errors.Add(ex.Message);
            }
            return errors.ToArray();
        }
    }
}
