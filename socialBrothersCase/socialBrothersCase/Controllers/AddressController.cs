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
    public class AddressController : ControllerBase
    {
        private AddressesContext _addressesContext;
        public AddressController(AddressesContext addressesContext)
        {
            _addressesContext = addressesContext;
        }

        // GET: api/<AddressController>
        /// <summary>
        /// Retrieves all Addresses if Filters are untouched
        /// FilterOperators are as follows:
        /// 0: Equals                   5: Less then or equal
        /// 1: Doesnt Equal             6: Contains
        /// 2: Greater then             7: Starts with
        /// 3: Greater then or equal    8: Ends with
        /// 4: Less then
        /// </summary>
        /// <param name="OrderByProperty"></param>
        /// <param name="filterByProperty"></param>
        /// <param name="filterOperator"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Address> Get([FromQuery] string OrderByProperty = "Street", [FromQuery] string filterByProperty = "Street", [FromQuery] DynamicExpressions.FilterOperator filterOperator = DynamicExpressions.FilterOperator.Contains, [FromQuery] string filterValue = "")
        {
            var propertyGetter = DynamicExpressions.DynamicExpressions.GetPropertyGetter<Address>(OrderByProperty);
            var predicate = DynamicExpressions.DynamicExpressions.GetPredicate<Address>(filterByProperty, filterOperator, filterValue);
            return _addressesContext.Adresses.AsQueryable().Where(predicate).OrderBy(propertyGetter);
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            //TODO add catch if there is no adress with given ID
            
            if (_addressesContext.Adresses.Any(a => a.Id == id))
            {
                return Ok(_addressesContext.Adresses.First(a => a.Id == id));
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult Post([FromBody] Address newAdress)
        {
            if (_addressesContext.Adresses.Any(a => a.Id == newAdress.Id))
            {
                return BadRequest();
            }

            _addressesContext.Add(newAdress);
            _addressesContext.SaveChanges();
            return Ok("added new adress");
        }

        // PUT api/<AddressController>/5
        [HttpPut]
        public void Put([FromBody] Address newValues)
        {
            //TODO add check thing idk anymore
            _addressesContext.Update(newValues);
            _addressesContext.SaveChanges();
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            //TODO add check if adress exists
            Address toBeDeleted = _addressesContext.Adresses.Where(a => a.Id == id).First();
            _addressesContext.Remove(toBeDeleted);
            _addressesContext.SaveChanges();
        }
    }
}
