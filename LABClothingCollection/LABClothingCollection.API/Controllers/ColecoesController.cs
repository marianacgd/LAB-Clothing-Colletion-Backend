using AutoMapper;
using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Enums;
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

        /// <summary>
        /// Exibe lista de todas coleções cadastradas no sistema.
        /// </summary>
        /// <returns>Exibe lista de todas as coleções.</returns>
        /// <response code="200">Sucesso no retorno da lista de coleções!</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ColecaoReadDTO>> Get([FromQuery] StatusEnum? status)
        {
            try
            {
                var colecaoModels = lABClothingCollectionDbContext.Colecoes
                                       .Include(u => u.Responsavel)
                                       .ToList();

                if (status.HasValue)
                {
                    colecaoModels = colecaoModels.Where(w => w.StatusSistema == status!).ToList();
                }

                var colecoesDTO = mapper.Map<List<ColecaoReadDTO>>(colecaoModels);
                return Ok(colecoesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Busca o cadastro de uma determinada coleção, a partir do identificador informado.
        /// </summary>
        /// <param name="identificador">Id da Coleção</param>
        /// <returns>Retorno do objeto Coleção</returns>
        /// <response code="200">Sucesso no retorno do objeto coleção!</response>
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
                var colecaoModel = lABClothingCollectionDbContext.Colecoes
                                       .Include(u => u.Responsavel)
                                       .Where(w => w.Id == identificador)
                                       .FirstOrDefault();

                if (colecaoModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                var colecaoDTO = RetornarColecaoResponse(colecaoModel);
                return Ok(colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Inclui uma nova coleção no sistema.
        /// </summary>
        /// <param name="colecaoCreateDTO">Objeto Coleção</param>
        /// <returns>Criação da Coleção</returns>
        /// <response code="201">Objeto Coleção postado na lista !</response>
        /// <response code="400">Dados Inválidos !</response>
        /// <response code="409">Já existe um registro com esse dado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                var colecaoDTO = RetornarColecaoResponse(colecaoModel);

                return CreatedAtAction(nameof(Post), colecaoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Altera o cadastro de uma coleção, a partir do identificador fornecido.
        /// </summary>
        /// <param name="identificador">Id da Coleção</param>
        /// <param name="colecaoUpdateDTO">Objeto com as novas caracteristicas da Coleção</param>
        /// <returns>Atualização da Coleção</returns>
        /// <response code="200">Atualização da coleção realizada com sucesso !</response>
        /// <response code="400">Dados inválidos !</response>
        /// <response code="404">Id não encontrado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPut("{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                                        .Exists(e => e.Nome.ToLower() == colecaoUpdateDTO.Nome.ToLower() && e.Id != identificador);

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


        /// <summary>
        /// Altera/Atualiza o status de uma coleção, a partir do identificador fornecido.
        /// </summary>
        /// <param name="identificador">Id da Coleção</param>
        /// <param name="colecaoUpdateStatusDTO">Objeto com as novas caracteristicas da Coleção</param>
        /// <returns>Atualização do Status da Coleção</returns>
        /// <response code="200">Atualização do status da coleção realizada com sucesso !</response>
        /// <response code="400">Dados inválidos !</response>
        /// <response code="404">Id não encontrado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPut("{identificador}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Remove do cadastro a coleção informada no identificador da requisição.
        /// </summary>
        /// <param name="identificador">Id da Coleção</param>
        /// <returns>Remoção da coleção da lista !</returns>
        /// <response code="204">Coleção removida com sucesso !</reponse>
        /// <response code="404">Coleção não encontrada !</reponse>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpDelete("{identificador}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int identificador)
        {
            try
            {
                var colecaoModel = lABClothingCollectionDbContext.Colecoes
                                            .Include(u => u.Modelos)
                                            .Where(w => w.Id == identificador && w.StatusSistema == StatusEnum.Inativo)
                                            .FirstOrDefault();

                if (colecaoModel == null || colecaoModel.Modelos!.Count > 0)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                lABClothingCollectionDbContext.Colecoes.Remove(colecaoModel);
                lABClothingCollectionDbContext.SaveChanges();

                return StatusCode(204);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        private ColecaoReadDTO RetornarColecaoResponse(ColecaoModel colecaoModel)
        {
            return mapper.Map<ColecaoReadDTO>(colecaoModel);
        }

        private UsuarioModel? BuscarUsuario(int responsavelId)
        {
            return lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == responsavelId).FirstOrDefault();
        }
    }
}
