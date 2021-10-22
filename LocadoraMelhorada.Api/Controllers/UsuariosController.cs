using LocadoraMelhorada.Domain.Commands.Inputs.Usuario;
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
    [Route("api/v1/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository<long> _repository;
        private readonly UsuarioHandler _handler;

        public UsuariosController(IUsuarioRepository<long> repository, UsuarioHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [AllowAnonymous]
        [HttpPost]
        public ICommandResult InserirUsuario([FromBody] AdicionarUsuarioCommand command)
        {
            return _handler.Handle(command);
        }

        [HttpPut]
        [Route("{id}")]
        public ICommandResult AtualizarUsuario([FromRoute] long id, [FromBody] AtualizarUsuarioCommand command)
        {
            command.Id = id;
            return _handler.Handle(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public ICommandResult ExcluirUsuario([FromRoute] long id)
        {
            var command = new ExcluirUsuarioCommand() { Id = id };
            return _handler.Handle(command);
        }

        [HttpGet]
        public List<UsuarioQueryResult> ListarUsuarios()
        {
            return _repository.Listar();
        }

        [HttpGet]
        [Route("{id}")]
        public UsuarioQueryResult ObterUsuario([FromRoute] long id)
        {
            return _repository.Obter(id);
        }
    }
}
