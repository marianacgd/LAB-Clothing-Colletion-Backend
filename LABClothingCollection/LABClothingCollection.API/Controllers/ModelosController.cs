using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Modelos;
using LABClothingCollection.API.Enums;
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

        [HttpGet]
        public ActionResult<IEnumerable<ModeloReadDTO>> Get([FromQuery] LayoutEnum? layout)
        {
            var modeloModels = lABClothingCollectionDbContext.Modelos
                                   .Include(c => c.Colecao)
                                   .ToList();

            if (layout.HasValue)
            {
                modeloModels = modeloModels.Where(w => w.Layout == layout!).ToList();
            }

            var modeloReadDTOs = mapper.Map<List<ModeloReadDTO>>(modeloModels);
            return Ok(modeloReadDTOs);
        }

        [HttpGet("{identificador}")]
        public ActionResult<ColecaoReadDTO> Get(int identificador)
        {
            var modeloModel = lABClothingCollectionDbContext.Modelos
                                   .Include(c => c.Colecao)
                                   .Where(w => w.Id == identificador)
                                   .FirstOrDefault();

            if (modeloModel == null)
            {
                return NotFound(new { erro = "Registro não encontrado" });
            }

            var modeloReadDTO = RetornarModeloResponse(modeloModel);
            return Ok(modeloReadDTO);
        }


        [HttpPut("{identificador}")]
        public ActionResult<ModeloReadDTO> Put(int identificador, [FromBody] ModeloDTO modeloUpdateDTO)
        {
            try
            {
                var modeloModel = lABClothingCollectionDbContext.Modelos
                                   .Include(c => c.Colecao)
                                   .Include(u => u.Colecao.Responsavel)
                                   .ToList()
                                   .Find(f => f.Id == identificador);

                if (modeloModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                var nomeExiste = lABClothingCollectionDbContext.Modelos                                  
                                        .ToList()
                                        .Exists(e => e.Nome.ToLower() == modeloUpdateDTO.Nome.ToLower() && e.Id != identificador);

                if (nomeExiste)
                {
                    return Conflict(new { erro = "Informe outro nome de modelo" });
                }

                modeloModel = mapper.Map(modeloUpdateDTO, modeloModel);

                var colecaoModel = BuscarColecao(modeloUpdateDTO.ColecaoId);
                modeloModel.Colecao = colecaoModel!;

                if (!TryValidateModel(colecaoModel!, nameof(colecaoModel)))
                {
                    return BadRequest(new { erro = "Dados com erros" });
                }

                lABClothingCollectionDbContext.Modelos.Update(modeloModel);
                lABClothingCollectionDbContext.SaveChanges();
                var modeloReadDTO = RetornarModeloResponse(modeloModel);
               
                return Ok(modeloReadDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
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
                                                 .Include(r => r.Responsavel)
                                                 .Where(w => w.Id == responsavelId).FirstOrDefault();
        }
    }
}
