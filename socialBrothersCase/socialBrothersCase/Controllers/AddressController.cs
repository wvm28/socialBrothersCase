using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using socialBrothersCase.ApiServices;
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
        private RadarService _radarService;
        public AddressController(AddressesContext addressesContext, RadarService radarService)
        {
            _addressesContext = addressesContext;
            _radarService = radarService;
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
        /// <summary>
        /// Gets an addres by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
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
        /// <summary>
        /// Creates a new address if given ID is unique, or if no ID is given
        /// </summary>
        /// <param name="newAdress"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates given address to new values, if na address is found with given ID it will create one
        /// </summary>
        /// <param name="newValues"></param>
        [HttpPut]
        public void Put([FromBody] Address newValues)
        {
            //TODO add check thing idk anymore
            
            if (_addressesContext.Adresses.Any(a => a.Id == newValues.Id))
            {
                _addressesContext.Update(newValues);
            } else
            {
                _addressesContext.Add(newValues);
            }
            _addressesContext.SaveChanges();
        }

        // DELETE api/<AddressController>/5
        /// <summary>
        /// Deletes the address that responds to the given id. If no address with given ID exist it returns a not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_addressesContext.Adresses.Any(a => a.Id == id))
            {
                Address toBeDeleted = _addressesContext.Adresses.Where(a => a.Id == id).First();
                _addressesContext.Remove(toBeDeleted);
                _addressesContext.SaveChanges();
                return Ok();
            } else
            {
                return NotFound();
            }
            
        }

        // GET: api/<AddressController>/getDistance/
        [HttpGet]
        [Route("getDistance")]
        ///<summary>
        /// Uses the given ID's to request coordinates for the responding addresses. After that it will request the distance inbetween both sets of coordinates.
        /// All three of these requests go the Radar API
        ///</summary>
        ///<param name="startingId"></param>
        ///<param name="finishId"></param>
        public async Task<IActionResult> GetDistanceAsync([FromQuery] Guid startId, [FromQuery] Guid finishId)
        {
            if (_addressesContext.Adresses.Any(a => a.Id == startId) != false || _addressesContext.Adresses.Any(a => a.Id == finishId) != false)
            {
                var startAddress = _addressesContext.Adresses.First(a => a.Id == startId);
                Coordinates startAddressCoords = await _radarService.GetCoordinatesAsync(startAddress);

                var finishAddress = _addressesContext.Adresses.First(a => a.Id == finishId);
                Coordinates finishAddressCoords = await _radarService.GetCoordinatesAsync(finishAddress);

                var result = _radarService.GetDistance(startAddressCoords, finishAddressCoords);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            return Ok("Tada");
        }
    }
}
