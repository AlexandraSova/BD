using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineShopRestServer.Models;
using OnlineShopRestServer.Repositories;



namespace OnlineShopRestServer.Controllers
{
    [Route("api/item")]
    public class ItemController : Controller
    {
        ShopContext db;
        ItemRepository repository;

        public ItemController()
        {
            db = new ShopContext();
            repository = new ItemRepository(db);
        }

        // GET api/item
        // GET api/item/?id=2
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

        // GET api/item/2
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


        // POST api/item
        [HttpPost]
        public IActionResult Post([FromBody] JObject data)
        {
            try
            {
                item model = (item)data.ToObject(typeof(item));
                return Json(repository.Insert(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // PUT api/item/2
        [HttpPut]
        public IActionResult Put([FromBody] JObject data)
        {
            try
            {
                item model = (item)data.ToObject(typeof(item));
                return Json(repository.Update(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // DELETE api/item/2
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
