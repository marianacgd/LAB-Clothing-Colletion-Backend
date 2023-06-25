using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Modelos;
using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LABClothingCollection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly LABClothingCollectionDbContext lABClothingCollectionDbContext;
        private readonly IMapper mapper;

        public ModelosController(LABClothingCollectionDbContext lABClothingCollectionDbContext, IMapper mapper)
        {
            this.lABClothingCollectionDbContext = lABClothingCollectionDbContext;
            this.mapper = mapper;
        }


        [HttpPost]
        public ActionResult<ModeloReadDTO> Post([FromBody] ModeloDTO modeloCreateDTO)
        {

            try
            {
                var modeloModel = mapper.Map<ModeloModel>(modeloCreateDTO);
                var colecaoModel = BuscarColecao(modeloCreateDTO.ColecaoId);

                modeloModel.Colecao = colecaoModel!;

                if (!TryValidateModel(modeloModel, nameof(modeloModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                if (lABClothingCollectionDbContext.Colecoes.ToList().Exists(e => e.Nome.ToLower() == modeloCreateDTO.Nome.ToLower()))
                {
                    return Conflict(new { erro = "Nome do modelo já cadastrado" });
                }

                lABClothingCollectionDbContext.Modelos.Add(modeloModel);
                lABClothingCollectionDbContext.SaveChanges();
                var modeloReadDTO = RetornarModeloResponse(modeloModel);

                return CreatedAtAction(nameof(Post), modeloReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/<ModelosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ModelosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/<ModelosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModelosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private ModeloReadDTO RetornarModeloResponse(ModeloModel modeloModel)
        {
            return mapper.Map<ModeloReadDTO>(modeloModel);
        }

        private ColecaoModel? BuscarColecao(int responsavelId)
        {
            return lABClothingCollectionDbContext.Colecoes
                                                 .Include(u => u.Responsavel)
                                                 .Where(w => w.Id == responsavelId).FirstOrDefault();
        }
    }
}
