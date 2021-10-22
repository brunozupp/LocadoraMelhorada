using LocadoraMelhorada.Domain.Commands.Inputs.Voto;
using LocadoraMelhorada.Domain.Handlers;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Domain.Query;
using LocadoraMelhorada.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace LocadoraMelhorada.Api.Controllers
{
    [Produces(Application.Json)]
    [Consumes(Application.Json)]
    [Authorize]
    [Route("api/v1/votos")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly IVotoRepository<long> _repository;
        private readonly VotoHandler _handler;

        public VotosController(IVotoRepository<long> repository, VotoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        public ICommandResult InserirVoto([FromBody] AdicionarVotoCommand command)
        {
            return _handler.Handle(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public ICommandResult ExcluirVoto([FromRoute] long id)
        {
            return _handler.Handle(new ExcluirVotoCommand() { Id = id });
        }

        [HttpGet]
        [Route("")]
        public List<VotoQueryResult> ListarVotos()
        {
            return _repository.Listar();
        }

        [HttpGet]
        [Route("usuarios/{usuarioId}")]
        public List<VotoDoUsuarioQueryResult> ListarVotosPorUsuario([FromRoute] long usuarioId)
        {
            return _repository.ListarPorUsuario(usuarioId);
        }
    }
}
