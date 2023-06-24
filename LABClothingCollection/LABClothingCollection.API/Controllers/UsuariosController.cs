using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LABClothingCollection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly LABClothingCollectionDbContext lABClothingCollectionDbContext;

        public UsuariosController(LABClothingCollectionDbContext lABClothingCollectionDbContext)
        {
            this.lABClothingCollectionDbContext = lABClothingCollectionDbContext;
        }

        [HttpGet]
        public IEnumerable<UsuarioModel> Get()
        {
            return lABClothingCollectionDbContext.Usuarios;
        }

        [HttpGet("{id}")]
        public UsuarioModel Get(int id)
        {
            return lABClothingCollectionDbContext.Usuarios.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] UsuarioModel usuario)
        {
            lABClothingCollectionDbContext.Usuarios.Add(usuario);
            lABClothingCollectionDbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UsuarioModel usuario)
        {
            usuario.Id = id;
            lABClothingCollectionDbContext.Usuarios.Update(usuario);
            lABClothingCollectionDbContext.SaveChanges();
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usuario = lABClothingCollectionDbContext.Usuarios.Find(id);

            lABClothingCollectionDbContext.Usuarios.Remove(usuario);
            lABClothingCollectionDbContext.SaveChanges();
        }
    }
}
