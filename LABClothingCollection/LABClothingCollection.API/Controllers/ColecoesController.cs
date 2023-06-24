using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<ColecaoReadDTO> Post([FromBody] ColecaoCreateDTO colecaoCreateDTO)
        {
            try
            {
                var colecaoModel = mapper.Map<ColecaoModel>(colecaoCreateDTO);
                var responsavelModel = lABClothingCollectionDbContext.Usuarios.Where(w => w.Id== colecaoCreateDTO.ResponsavelId).FirstOrDefault();

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
                var colecaoDTO = RetornarColecaoResponse(colecaoModel);

                return CreatedAtAction(nameof(Post), colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/<ColecoesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
    }
}
