using LocadoraMelhorada.Domain.Commands.Inputs.Usuario;
using LocadoraMelhorada.Domain.Commands.Outputs;
using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Handlers
{
    public class UsuarioHandler : ICommandHandler<AdicionarUsuarioCommand>, ICommandHandler<AtualizarUsuarioCommand>, ICommandHandler<ExcluirUsuarioCommand>
    {
        private readonly IUsuarioRepository<long> _repository;

        public UsuarioHandler(IUsuarioRepository<long> repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new UsuarioCommandResult(false, "Possui erros de validação", command.Notifications);

            Usuario usuario = new Usuario(command.Nome, command.Login, command.Senha);

            var id = _repository.Inserir(usuario);

            usuario.AtualizarId(id);

            return new UsuarioCommandResult(true, "Usuário cadastrado com sucesso!", new
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login
            });
        }

        public ICommandResult Handle(AtualizarUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new UsuarioCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new UsuarioCommandResult(false, "Este usuário não existe.", command.Notifications);

            Usuario usuario = new Usuario(command.Id, command.Nome, command.Login, command.Senha);

            _repository.Atualizar(usuario);

            return new UsuarioCommandResult(true, "Usuário atualizado com sucesso.", usuario);
        }

        public ICommandResult Handle(ExcluirUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new UsuarioCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_repository.CheckId(command.Id))
                return new UsuarioCommandResult(false, "Este usuário não existe.", command.Notifications);

            _repository.Excluir(command.Id);

            return new UsuarioCommandResult(true, "Usuário excluído com sucesso", new
            {
                Id = command.Id
            });
        }
    }
}
