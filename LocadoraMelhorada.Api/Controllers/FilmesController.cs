using LocadoraMelhorada.Domain.Commands.Inputs.Filme;
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
    [Route("api/v1/filmes")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeRepository<long> _repository;
        private readonly FilmeHandler _handler;

        public FilmesController(IFilmeRepository<long> repository, FilmeHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        public ICommandResult InserirFilme([FromBody] AdicionarFilmeCommand command)
        {
            return _handler.Handle(command);
        }

        [HttpPut("{id}")]
        public ICommandResult AtualizarFilme([FromRoute] long id, [FromBody] AtualizarFilmeCommand command)
        {
            command.Id = id;
            return _handler.Handle(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public ICommandResult ExcluirFilme([FromRoute] long id)
        {
            var command = new ExcluirFilmeCommand() { Id = id };
            return _handler.Handle(command);
        }

        [HttpGet]
        [Route("")]
        public List<FilmeQueryResult> ListarFilmes()
        {
            return _repository.Listar();
        }

        [HttpGet]
        [Route("{id}")]
        public FilmeQueryResult ObterFilme([FromRoute] long id)
        {
            return _repository.Obter(id);
        }
    }
}
