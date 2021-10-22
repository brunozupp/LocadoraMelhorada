using LocadoraMelhorada.Domain.Commands.Inputs.Autenticacao;
using LocadoraMelhorada.Domain.Handlers;
using LocadoraMelhorada.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LocadoraMelhorada.Api.Controllers
{
    [Produces(Application.Json)]
    [Consumes(Application.Json)]
    [Route("api/v1/autenticacao")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoHandler _handler;

        public AutenticacaoController(AutenticacaoHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("signin")]
        public ICommandResult Autenticar([FromBody] AutenticacaoCommand command)
        {
            return _handler.Handle(command);
        }
    }
}
