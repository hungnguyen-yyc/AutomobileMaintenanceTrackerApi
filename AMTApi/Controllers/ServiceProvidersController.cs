﻿using System;
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
    public class ServiceProvidersController : Controller
    {
        IRepository<ServiceProviderModel> _repo = Repository<ServiceProviderModel>.Instance;

        [HttpGet]
        public IEnumerable<ServiceProviderModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]
        public string Post([FromBody]PostServiceProviderModel value)
        {
            var provider = new ServiceProviderModel()
            {
                ShopName = value.ShopName,
                Address = value.Address,
                Phone = value.Phone
            };
            if (provider.Validate().Length == 0)
            {
                _repo.Create(provider);
                return JsonConvert.SerializeObject(new ResultModel($"New Provider Created!", false));
            }
            return JsonConvert.SerializeObject(new ResultModel(provider.Validate().Aggregate((a,b) => $"{a} | {b}"), true));
        }

        [HttpPut]
        public string Put([FromBody]ServiceProviderModel value)
        {
            if (value.Validate().Length == 0)
            {
                _repo.Update(value);
                return JsonConvert.SerializeObject(new ResultModel($"Provider {value.ShopName} Updated!", false));
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
                return JsonConvert.SerializeObject(new ResultModel($"Provider {veh.ShopName} Deleted!", false));
            }
            return JsonConvert.SerializeObject(new ResultModel("Provider Not Found", true));

        }
    }
}
