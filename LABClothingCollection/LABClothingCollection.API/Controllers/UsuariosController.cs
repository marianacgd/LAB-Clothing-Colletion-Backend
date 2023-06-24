using AutoMapper;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Enums;
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
        private readonly IMapper mapper;

        public UsuariosController(LABClothingCollectionDbContext lABClothingCollectionDbContext, IMapper mapper)
        {
            this.lABClothingCollectionDbContext = lABClothingCollectionDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioReadDTO>> Get([FromQuery] StatusEnum? status)
        {
            var usuarioModels = lABClothingCollectionDbContext.Usuarios.ToList();

            if (status.HasValue)
            {
                usuarioModels = usuarioModels.Where(w => w.Status == status!).ToList();
            }

            var usuarioDTO = mapper.Map<List<UsuarioReadDTO>>(usuarioModels);
            return Ok(usuarioDTO);
        }

        [HttpGet("{id}")]
        public UsuarioModel Get(int id)
        {
            return lABClothingCollectionDbContext.Usuarios.Find(id);
        }

        [HttpPost]
        public ActionResult<UsuarioReadDTO> Post([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            try
            {
                var usuarioModel = mapper.Map<UsuarioModel>(usuarioCreateDTO);

                if (!TryValidateModel(usuarioModel, nameof(usuarioModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                if (lABClothingCollectionDbContext.Usuarios.ToList().Exists(e => e.Documento == usuarioCreateDTO.Documento))
                {
                    return Conflict(new { erro = "CPNJ ou CPF já cadastrado" });
                }

                if (lABClothingCollectionDbContext.Usuarios.ToList().Exists(e => e.Email.ToLower() == usuarioCreateDTO.Email.ToLower()))
                {
                    return Conflict(new { erro = "E-mail já cadastrado" });
                }

                lABClothingCollectionDbContext.Usuarios.Add(usuarioModel);
                lABClothingCollectionDbContext.SaveChanges();
                var usuarioDTO = RetornarUsuarioResponse(usuarioModel);

                return CreatedAtAction(nameof(Post), usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut("{identificador}")]
        public ActionResult<UsuarioReadDTO> Put([FromRoute] int identificador, [FromBody] UsuarioUpdateDTO usuarioUpdateDTO)
        {
            try
            {
                var usuarioModel = lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == identificador).FirstOrDefault();

                if (usuarioModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                usuarioModel = mapper.Map(usuarioUpdateDTO, usuarioModel);

                if (!TryValidateModel(usuarioModel, nameof(usuarioModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                lABClothingCollectionDbContext.Usuarios.Update(usuarioModel);
                lABClothingCollectionDbContext.SaveChanges();

                var usuarioDTO = RetornarUsuarioResponse(usuarioModel);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut("{identificador}/status")]
        public ActionResult<UsuarioReadDTO> Put([FromRoute] int identificador, [FromBody] UsuarioUpdateStatusDTO usuarioUpdateStatusDTO)
        {
            try
            {
                if (!Enum.IsDefined(typeof(StatusEnum), usuarioUpdateStatusDTO.Status))
                    return BadRequest(new { erro = "Status não existente" });

                var usuarioModel = lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == identificador).FirstOrDefault();

                if (usuarioModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                lABClothingCollectionDbContext.Usuarios.Update(usuarioModel);
                lABClothingCollectionDbContext.SaveChanges();
                var usuarioDTO = RetornarUsuarioResponse(usuarioModel);

                return Ok(usuarioDTO);
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

        private UsuarioReadDTO RetornarUsuarioResponse(UsuarioModel usuarioModel)
        {
            return mapper.Map<UsuarioReadDTO>(usuarioModel);
        }

    }
}
