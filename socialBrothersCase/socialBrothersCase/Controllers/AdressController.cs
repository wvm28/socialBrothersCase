using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using socialBrothersCase.DatabaseContexts;
using socialBrothersCase.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace socialBrothersCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private AdressesContext _adressesContext;
        public AdressController(AdressesContext adressesContext)
        {
            _adressesContext = adressesContext;

        }

        // GET: api/<AdressController>
        [HttpGet]
        public IEnumerable<Adress> Get()
        {
            //TODO add filters/orderbys
            return _adressesContext.Adresses.ToList();
        }

        // GET api/<AdressController>/5
        [HttpGet("{id}")]
        public Adress Get(Guid id)
        {
            //TODO add catch if there is no adress with given ID
            return _adressesContext.Adresses.Where(a => a.Id == id).First();
        }

        // POST api/<AdressController>
        [HttpPost]
        public string Post([FromBody] Adress newAdress)
        {
            //TODO add validation
            _adressesContext.Add(newAdress);
            _adressesContext.SaveChanges();
            return "added new adress";
        }

        // PUT api/<AdressController>/5
        [HttpPut]
        public void Put([FromBody] Adress newValues)
        {
            /*Adress oldValues = _adressesContext.Adresses.Where(a => a.Id == id).First();
            oldValues = newValues;*/

            _adressesContext.Update(newValues);
            _adressesContext.SaveChanges();
        }

        // DELETE api/<AdressController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Adress toBeDeleted = _adressesContext.Adresses.Where(a => a.Id == id).First();
            _adressesContext.Remove(toBeDeleted);
            _adressesContext.SaveChanges();
        }
    }
}
