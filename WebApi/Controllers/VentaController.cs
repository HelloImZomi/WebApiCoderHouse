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
    public class VentaController : ControllerBase
    {
        // GET: api/Venta
        [HttpGet(Name = "ObtenerVentas")]
        public IEnumerable<Venta> Get()
        {
            return ADO_Venta.GetAll();
        }

        // GET: api/Venta/5
        [HttpGet("{id}", Name = "BuscarVenta")]
        public Venta Get(int id)
        {
            return ADO_Venta.GetById(id);
        }

        // POST: api/Venta
        [HttpPost]
        public void Post([FromBody] Venta venta)
        {
            ADO_Venta.Store(venta);
        }

        // PUT: api/Venta/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Venta venta)
        {
            ADO_Venta.Update(id, venta);
        }

        // DELETE: api/Venta/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ADO_Venta.Delete(id);
        }
    }
}
