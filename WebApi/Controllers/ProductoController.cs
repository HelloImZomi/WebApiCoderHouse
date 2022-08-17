using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/Producto
        [HttpGet(Name = "ObtenerProductos")]
        public IEnumerable<Producto> Get()
        {
            return ADO_Producto.GetAll();
        }

        // GET: api/Producto/5
        [HttpGet("{id}", Name = "BuscarProducto")]
        public Producto Get(int id)
        {
            return ADO_Producto.GetById(id);
        }

        // POST: api/Producto
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}