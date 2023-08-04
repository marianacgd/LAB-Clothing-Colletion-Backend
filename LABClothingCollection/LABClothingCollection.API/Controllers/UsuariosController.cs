using AutoMapper;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Enums;
using LABClothingCollection.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//controllers são responsáveis por receber as requisições dos clientes,
//processar a lógica de negócio e retornar as respostas adequadas.
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

        /// <summary>
        /// Exibe lista de todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Exibe lista de todos os usuários.</returns>
        /// <response code="200">Sucesso no retorno da lista de usuários!</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<UsuarioReadDTO>> Get([FromQuery] StatusEnum? status)
        {
            try
            {
                var usuarioModels = lABClothingCollectionDbContext.Usuarios.ToList();

                if (status.HasValue)
                {
                    usuarioModels = usuarioModels.Where(w => w.Status == status!).ToList();
                }

                var usuarioDTO = mapper.Map<List<UsuarioReadDTO>>(usuarioModels);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Busca o cadastro de um determinado usuário, a partir do identificador informado.
        /// </summary>
        /// <param name="identificador">Id do Usuário</param>
        /// <returns>Retorno do objeto Usuário</returns>
        /// <response code="200">Sucesso no retorno do objeto usuário!</response>
        /// <response code="404">Id inválido !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpGet("{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioReadDTO> Get(int identificador)
        {
            try
            {
                var usuarioModel = lABClothingCollectionDbContext.Usuarios.Find(identificador);

                if (usuarioModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                var usuarioDTO = RetornarUsuarioResponse(usuarioModel);
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Inclui um novo usuário no sistema.
        /// </summary>
        /// <param name="usuarioCreateDTO">Objeto Usuário</param>
        /// <returns>Criação do Usuário</returns>
        /// <response code="201">Objeto Usuário postado na lista !</response>
        /// <response code="400">Dados Inválidos !</response>
        /// <response code="409">Já existe um registro com esse dado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Altera o cadastro de um usuário, a partir do identificador fornecido.
        /// </summary>
        /// <param name="identificador">Id do Usuário</param>
        /// <param name="usuarioUpdateDTO">Objeto com as novas caracteristicas do Usuário</param>
        /// <returns>Atualização do Usuário</returns>
        /// <response code="200">Atualização do usuário realizada com sucesso !</response>
        /// <response code="400">Dados inválidos !</response>
        /// <response code="404">Id não encontrado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPut("{identificador}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Altera/Atualiza o status de um usuário, a partir do identificador fornecido.
        /// </summary>
        /// <param name="identificador">Id do Usuário</param>
        /// <param name="usuarioUpdateStatusDTO">Objeto com as novas caracteristicas do Usuário</param>
        /// <returns>Atualização do Status do Usuário</returns>
        /// <response code="200">Atualização do status do usuário realizada com sucesso !</response>
        /// <response code="400">Dados inválidos !</response>
        /// <response code="404">Id não encontrado !</response>
        /// <response code="500">Erro de comunicação com o servidor !</response>
        [HttpPut("{identificador}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioReadDTO> Put([FromRoute] int identificador, [FromBody] UsuarioUpdateStatusDTO usuarioUpdateStatusDTO)
        {
            try
            {
                var usuarioModel = lABClothingCollectionDbContext.Usuarios.Where(w => w.Id == identificador).FirstOrDefault();

                if (usuarioModel == null)
                {
                    return NotFound(new { erro = "Registro não encontrado" });
                }

                usuarioModel = mapper.Map(usuarioUpdateStatusDTO, usuarioModel);

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

        private UsuarioReadDTO RetornarUsuarioResponse(UsuarioModel usuarioModel)
        {
            return mapper.Map<UsuarioReadDTO>(usuarioModel);
        }

    }
}
