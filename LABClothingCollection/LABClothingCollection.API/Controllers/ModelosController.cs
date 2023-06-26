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

        /// <summary>
        /// Exibe lista de todos modelos cadastrados no sistema.
        /// </summary>
        /// <returns>Exibe lista de todos os modelos.</returns>
        /// <response code="200">Sucesso no retorno da lista de modelos!</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ModeloReadDTO>> Get([FromQuery] LayoutEnum? layout)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Busca o cadastro de um determinado modelo, a partir do identificador informado.
        /// </summary>
        /// <param name="identificador">Id do Modelo</param>
        /// <returns>Retorno do objeto Modelo</returns>
        /// <response code="200">Sucesso no retorno do objeto modelo!</response>
        /// <response code="404">Id inválido !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpGet("{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ColecaoReadDTO> Get(int identificador)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Inclui um novo modelo no sistema.
        /// </summary>
        /// <param name="modeloCreateDTO">Objeto Modelo</param>
        /// <returns>Criação do Modelo</returns>
        /// <response code="201">Objeto Modelo postado na lista !</response>
        /// <response code="400">Dados Inválidos !</response>
        /// <response code="409">Já existe um registro com esse dado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Altera o cadastro de um modelo, a partir do identificador fornecido.
        /// </summary>
        /// <param name="identificador">Id do Modelo</param>
        /// <param name="modeloUpdateDTO">Objeto com as novas caracteristicas do Modelo</param>
        /// <returns>Atualização do Modelo</returns>
        /// <response code="200">Atualização do modelo realizada com sucesso !</response>
        /// <response code="400">Dados inválidos !</response>
        /// <response code="404">Id não encontrado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPut("{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Remove do cadastro o modelo informado no identificador da requisição.
        /// </summary>
        /// <param name="identificador">Id do Modelo</param>
        /// <returns>Remoção do modelo da lista !</returns>
        /// <response code="204">Modelo removido com sucesso !</reponse>
        /// <response code="404">Modelo não encontrado !</reponse>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpDelete("{identificador}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int identificador)
        {
            try
            {
                var modeloModel = lABClothingCollectionDbContext.Modelos.Find(identificador);

                if (modeloModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                lABClothingCollectionDbContext.Modelos.Remove(modeloModel);
                lABClothingCollectionDbContext.SaveChanges();

                return StatusCode(204);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
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
