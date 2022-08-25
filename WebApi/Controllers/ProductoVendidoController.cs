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
    public class ProductoVendidoController : ControllerBase
    {
        // GET: api/ProductoVendido
        [HttpGet(Name = "ObtenerProductosVendidos")]
        public IEnumerable<ProductoVendido> Get()
        {
            return ADO_ProductoVendido.getAll();
        }

        // GET: api/ProductoVendido/5
        [HttpGet("{id}", Name = "BuscarProductoVendido")]
        public ProductoVendido Get(int id)
        {
            return ADO_ProductoVendido.GetById(id);
        }

        // POST: api/ProductoVendido
        [HttpPost]
        public void Post([FromBody] ProductoVendido productoVendido)
        {
            ADO_ProductoVendido.Store(productoVendido);
        }

        // PUT: api/ProductoVendido/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductoVendido productoVendido)
        {
            ADO_ProductoVendido.Update(id, productoVendido);
        }

        // DELETE: api/ProductoVendido/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ADO_ProductoVendido.Delete(id);
        }
    }
}
