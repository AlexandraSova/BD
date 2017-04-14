using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineShopRestServer.Models;
using OnlineShopRestServer.Repositories;

namespace OnlineShopRestServer.Controllers
{
    [Route("api/client")]
    public class ClientController : Controller
    {
        ShopContext db;
        ClientRepository repository;

        public ClientController()
        {
            db = new ShopContext();
            repository = new ClientRepository(db);
        }

        // GET api/client
        // GET api/client/?manager_id=2&address_id=1
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

        // GET api/client/2
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
        
        
        // POST api/client
        [HttpPost]
        public IActionResult Post([FromBody] JObject data)
        {
            try
            {
                client model = (client)data.ToObject(typeof(client));
                return Json(repository.Insert(model));
            }
            catch(Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }
        
        // PUT api/client/2
        [HttpPut]
        public IActionResult Put([FromBody] JObject data)
        {
            try
            {
                client model = (client)data.ToObject(typeof(client));
                return Json(repository.Update(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }
        
        // DELETE api/client/2
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
