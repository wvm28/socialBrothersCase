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
        /// <summary>
        /// Retrieves all Adresses if Filters are untouched
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
        public IEnumerable<Adress> Get([FromQuery] string OrderByProperty = "Street", [FromQuery] string filterByProperty = "Street", [FromQuery] DynamicExpressions.FilterOperator filterOperator = DynamicExpressions.FilterOperator.Contains, [FromQuery] string filterValue = "")
        {
            var propertyGetter = DynamicExpressions.DynamicExpressions.GetPropertyGetter<Adress>(OrderByProperty);
            var predicate = DynamicExpressions.DynamicExpressions.GetPredicate<Adress>(filterByProperty, filterOperator, filterValue);
            return _adressesContext.Adresses.AsQueryable().Where(predicate).OrderBy(propertyGetter);
        }

        // GET api/<AdressController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            //TODO add catch if there is no adress with given ID
            
            if (_adressesContext.Adresses.Any(a => a.Id == id))
            {
                return Ok(_adressesContext.Adresses.First(a => a.Id == id));
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<AdressController>
        [HttpPost]
        public IActionResult Post([FromBody] Adress newAdress)
        {
            if (_adressesContext.Adresses.Any(a => a.Id == newAdress.Id))
            {
                return BadRequest();
            }

            _adressesContext.Add(newAdress);
            _adressesContext.SaveChanges();
            return Ok("added new adress");
        }

        // PUT api/<AdressController>/5
        [HttpPut]
        public void Put([FromBody] Adress newValues)
        {
            //TODO add check thing idk anymore
            _adressesContext.Update(newValues);
            _adressesContext.SaveChanges();
        }

        // DELETE api/<AdressController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            //TODO add check if adress exists
            Adress toBeDeleted = _adressesContext.Adresses.Where(a => a.Id == id).First();
            _adressesContext.Remove(toBeDeleted);
            _adressesContext.SaveChanges();
        }
    }
}
