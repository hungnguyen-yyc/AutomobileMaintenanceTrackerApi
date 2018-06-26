using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTApi.Models;
using AMTDll;
using AMTDll.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMTApi.Controllers
{
    [Route("api/[controller]")]
    public class ServiceProvidersController : Controller
    {
        IRepository<ServiceProviderModel> _repo = new Repository<ServiceProviderModel>();

        [HttpGet]
        public IEnumerable<ServiceProviderModel> Get()
        {
            return _repo.Read();
        }

        [HttpPost]//use post vehicle to prevent id generate from client
        public void Post([FromBody]PostServiceProviderModel value)
        {
            var provider = new ServiceProviderModel()
            {
                ShopName = value.ShopName,
                Address = value.Address,
                Phone = value.Phone
            };
            _repo.Create(provider);
        }

        [HttpPut]
        public void Put([FromBody]ServiceProviderModel value)
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
