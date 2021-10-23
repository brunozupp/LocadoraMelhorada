using LocadoraMelhorada.Domain.Commands.Inputs.Filme;
using LocadoraMelhorada.Domain.Commands.Outputs;
using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Handlers
{
    public class FilmeHandler : ICommandHandler<AdicionarFilmeCommand>, ICommandHandler<AtualizarFilmeCommand>, ICommandHandler<ExcluirFilmeCommand>
    {
        private readonly IFilmeRepository<string> _repository;

        public FilmeHandler(IFilmeRepository<string> repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarFilmeCommand command)
        {
            if (!command.ValidarCommand())
                return new FilmeCommandResult(false, "Possui erros de validação", command.Notifications);

            Filme filme = new Filme(command.Titulo, command.Diretor);

            filme = _repository.Inserir(filme);
            //var id = _repository.Inserir(filme);
            //filme.AtualizarId(id);

            return new FilmeCommandResult(true, "Filme cadastrado com sucesso!", filme);
        }

        public ICommandResult Handle(AtualizarFilmeCommand command)
        {
            if (!command.ValidarCommand())
                return new FilmeCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new FilmeCommandResult(false, "Este filme não existe.", command.Notifications);

            Filme filme = new Filme(command.Id, command.Titulo, command.Diretor);

            _repository.Atualizar(filme);

            return new FilmeCommandResult(true, "Filme atualizado com sucesso!", filme);
        }

        public ICommandResult Handle(ExcluirFilmeCommand command)
        {
            if (!command.ValidarCommand())
                return new FilmeCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new FilmeCommandResult(false, "Este filme não existe.", new { });

            _repository.Excluir(command.Id);
            return new FilmeCommandResult(true, "Filme excluído com sucesso!", new
            {
                Id = command.Id
            });
        }
    }
}
