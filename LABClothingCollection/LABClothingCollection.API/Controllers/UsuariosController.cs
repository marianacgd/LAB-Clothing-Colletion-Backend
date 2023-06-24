using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Enums;
using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Extensions;
using System.Linq;

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
        public ActionResult Post([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            try
            {
                UsuarioModel usuarioModel = new()
                {
                    DataNascimento = usuarioCreateDTO.DataNascimento,
                    Documento = usuarioCreateDTO.Documento,
                    Email = usuarioCreateDTO.Email,
                    Genero = usuarioCreateDTO.Genero.GetDisplayName(),
                    NomeCompleto = usuarioCreateDTO.NomeCompleto,
                    Status = usuarioCreateDTO.Status,
                    Telefone = usuarioCreateDTO.Telefone,
                    Tipo = usuarioCreateDTO.Tipo
                };

                if (!TryValidateModel(usuarioModel, nameof(usuarioModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                if (lABClothingCollectionDbContext.Usuarios.ToList().Exists(e => e.Documento == usuarioCreateDTO.Documento))
                {
                    return Conflict(new { erro = "CPNJ ou CPF já cadastrados" });
                }

                lABClothingCollectionDbContext.Usuarios.Add(usuarioModel);
                lABClothingCollectionDbContext.SaveChanges();

                return CreatedAtAction(nameof(Post), new { identficador = usuarioModel.Id, tipo = usuarioModel.Tipo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut("{identificador}")]
        public ActionResult<UsuarioUpdateDTO> Put([FromRoute] int identificador, [FromBody] UsuarioUpdateDTO usuarioUpdateDTO)
        {
            try
            {
                var usuarioModel = lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == identificador).FirstOrDefault();

                if (usuarioModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                usuarioModel.NomeCompleto = usuarioUpdateDTO.NomeCompleto;
                usuarioModel.Genero = usuarioUpdateDTO.Genero.GetDisplayName();
                usuarioModel.DataNascimento = usuarioModel.DataNascimento;
                usuarioModel.Telefone = usuarioModel.Telefone;
                usuarioModel.Tipo = usuarioModel.Tipo;

                if (!TryValidateModel(usuarioModel, nameof(usuarioModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                lABClothingCollectionDbContext.Usuarios.Update(usuarioModel);
                lABClothingCollectionDbContext.SaveChanges();

                return Ok(usuarioUpdateDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

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
