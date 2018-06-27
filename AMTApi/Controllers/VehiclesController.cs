﻿using System;
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
        IRepository<VehicleModel> _repo;

        public VehiclesController(IRepository<VehicleModel> repo){
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<VehicleModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]
        public string Post([FromBody]PostVehicleModel value)
        {
            try
            {
                var vehicle = value.ToVehicleModel();
                var validation = ValidateService(vehicle);
                if (validation.Length == 0)
                {
                    _repo.Create(vehicle);
                    return JsonConvert.SerializeObject(new ResultModel("New Vehicle Created", false));
                }
                return JsonConvert.SerializeObject(new ResultModel(validation.Aggregate((a, b) => $"{a} | {b}"), true));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel(ex.Message, true));
            }
        }

        [HttpPut]
        public string Put([FromBody]VehicleModel value)
        {
            try
            {
                var validation = ValidateService(value);
                if (validation.Length == 0)
                {
                    _repo.Update(value);
                    return JsonConvert.SerializeObject(new ResultModel($"Vehicle {value.Plate} Updated!", false));
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
                    return JsonConvert.SerializeObject(new ResultModel($"Vehicle {veh.Plate} Deleted!", false));
                }
                return JsonConvert.SerializeObject(new ResultModel("Provider Not Found", true));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel(ex.Message, true));
            }
        }

        string[] ValidateService(VehicleModel vehicle)
        {
            var errors = new List<string>();
            try
            {
                errors.AddRange(vehicle.Validate());

                var vehicles = _repo.Read();
                if (vehicles.Where(veh => veh.Id != vehicle.Id).Any(veh => veh.Plate.Equals(vehicle.Plate)))
                    errors.Add($"Vehicle with license plate \"{vehicle.Plate}\" is already existed.");
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            return errors.ToArray();
        }
    }
}
