using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LABClothingCollection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColecoesController : ControllerBase
    {
        private readonly LABClothingCollectionDbContext lABClothingCollectionDbContext;
        private readonly IMapper mapper;

        public ColecoesController(LABClothingCollectionDbContext lABClothingCollectionDbContext, IMapper mapper)
        {
            this.lABClothingCollectionDbContext = lABClothingCollectionDbContext;
            this.mapper = mapper;
        }

        // GET: api/<ColecoesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ColecoesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<ColecaoReadDTO> Post([FromBody] ColecaoDTO colecaoCreateDTO)
        {
            try
            {
                var colecaoModel = mapper.Map<ColecaoModel>(colecaoCreateDTO);
                var responsavelModel = BuscarUsuario(colecaoCreateDTO.ResponsavelId);

                colecaoModel.Responsavel = responsavelModel!;

                if (!TryValidateModel(colecaoModel, nameof(colecaoModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                if (lABClothingCollectionDbContext.Colecoes.ToList().Exists(e => e.Nome.ToLower() == colecaoCreateDTO.Nome.ToLower()))
                {
                    return Conflict(new { erro = "Nome da coleção já cadastrado" });
                }

                lABClothingCollectionDbContext.Colecoes.Add(colecaoModel);
                lABClothingCollectionDbContext.SaveChanges();
                var colecaoDTO =  RetornarColecaoResponse(colecaoModel);

                return CreatedAtAction(nameof(Post), colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{identificador}")]
        public ActionResult<ColecaoReadDTO> Put(int identificador, [FromBody] ColecaoDTO colecaoUpdateDTO)
        {
            try
            {
                var colecaoModel = lABClothingCollectionDbContext.Colecoes
                                   .Include(u => u.Responsavel)
                                   .ToList()
                                   .Find(f => f.Id == identificador);

                if (colecaoModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                var nomeExiste = lABClothingCollectionDbContext.Colecoes
                                        .ToList()
                                        .Exists(e => e.Nome.ToLower() == colecaoUpdateDTO.Nome && e.Id != identificador);

                if (nomeExiste)
                {
                    return Conflict(new { erro = "Informe outro nome de coleção" });
                }

                colecaoModel = mapper.Map(colecaoUpdateDTO, colecaoModel);

                var responsavelModel = BuscarUsuario(colecaoUpdateDTO.ResponsavelId);
                colecaoModel.Responsavel = responsavelModel!;

                if (!TryValidateModel(colecaoModel, nameof(colecaoModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                lABClothingCollectionDbContext.Colecoes.Update(colecaoModel);
                lABClothingCollectionDbContext.SaveChanges();
                var colecaoDTO = RetornarColecaoResponse(colecaoModel);

                return Ok(colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{identificador}/status")]
        public ActionResult<ColecaoReadDTO> Put([FromRoute] int identificador, [FromBody] ColecaoUpdateStatusDTO colecaoUpdateStatusDTO)
        {
            try
            {
                var colecaoModel = lABClothingCollectionDbContext.Colecoes
                                   .Include(u => u.Responsavel)
                                   .ToList()
                                   .Find(f => f.Id == identificador);

                if (colecaoModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                colecaoModel = mapper.Map(colecaoUpdateStatusDTO, colecaoModel);

                lABClothingCollectionDbContext.Colecoes.Update(colecaoModel);
                lABClothingCollectionDbContext.SaveChanges();
                var colecaoDTO = RetornarColecaoResponse(colecaoModel);

                return Ok(colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }








        // DELETE api/<ColecoesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private ColecaoReadDTO RetornarColecaoResponse(ColecaoModel usuarioModel)
        {
            return mapper.Map<ColecaoReadDTO>(usuarioModel);
        }

        private UsuarioModel? BuscarUsuario(int responsavelId)
        {
            return lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == responsavelId).FirstOrDefault();
        }
    }
}
