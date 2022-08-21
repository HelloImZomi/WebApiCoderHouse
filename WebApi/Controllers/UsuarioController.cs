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
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "ObtenerUsuarios")]
        public IEnumerable<Usuario> Get()
        {
            return ADO_Usuario.GetAll();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}", Name = "BuscarUsuario")]
        public Usuario Get(int id)
        {
            return ADO_Usuario.GetById(id);
        }

        // POST: api/Usuario
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
