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
        // GET: api/Usuario/nombreUsuario/contraseña
        [HttpGet("{nombreUsuario}/{contraseña}", Name = "Auth")]
        public Usuario Auth(string nombreUsuario, string contraseña)
        {
            return ADO_Auth.login(nombreUsuario, contraseña);
        }

        [HttpGet(Name = "ObtenerUsuarios")]
        public IEnumerable<Usuario> Get()
        {
            return ADO_Usuario.GetAll();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}", Name = "BuscarUsuarioPorId")]
        public Usuario Get(int id)
        {
            return ADO_Usuario.GetById(id);
        }

        // GET: api/Usuario/nombreUsuario
        [HttpGet("{nombreUsuario}", Name = "BuscarUsuarioPorNombreUsuario")]
        public Usuario GetUserByName(string nombreUsuario)
        {
            return ADO_Usuario.GetByUserName(nombreUsuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public void Post([FromBody] Usuario usuario)
        {
            ADO_Usuario.Store(usuario);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            ADO_Usuario.Update(id, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ADO_Usuario.Delete(id);
        }
    }
}
