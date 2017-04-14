using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineShopRestServer.Models;
using OnlineShopRestServer.Repositories;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineShopRestServer.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        ShopContext db;
        ProductRepository repository;

        public ProductController()
        {
            db = new ShopContext();
            repository = new ProductRepository(db);
        }

        // GET api/product
        // GET api/product/?id=2
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

        // GET api/product/2
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


        // POST api/product
        [HttpPost]
        public IActionResult Post([FromBody] JObject data)
        {
            try
            {
                product model = (product)data.ToObject(typeof(product));
                return Json(repository.Insert(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // PUT api/product/2
        [HttpPut]
        public IActionResult Put([FromBody] JObject data)
        {
            try
            {
                product model = (product)data.ToObject(typeof(product));
                return Json(repository.Update(model));
            }
            catch (Npgsql.PostgresException)
            {
                return StatusCode(400);
            }
        }

        // DELETE api/product/2
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
