using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineShopRestServer.Models;
using OnlineShopRestServer.Repositories;

namespace OnlineShopRestServer.Controllers
{
    [Route("api/address")]
    public class AddressController : Controller
    {
        ShopContext db;
        AddressRepository repository;

        public AddressController()
        {
            db = new ShopContext();
            repository = new AddressRepository(db);
        }

        // GET api/address
        // GET api/address/?id=2
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (Request.Query.Count > 0)
                {
                    return Json(repository.SelectByFilter(Request.Query.ToList()));
                }

                return Json(repository.SelectAll());
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // GET api/address/2
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Json(repository.Select(id));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }


        // POST api/address
        [HttpPost]
        public IActionResult Post([FromBody] JObject data)
        {
            try
            {
                address model = (address)data.ToObject(typeof(address));
                return Json(repository.Insert(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // PUT api/address/2
        [HttpPut]
        public IActionResult Put([FromBody] JObject data)
        {
            try
            {
                address model = (address)data.ToObject(typeof(address));
                return Json(repository.Update(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // DELETE api/address/2
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Json(repository.Delete(id));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }
    }
}
