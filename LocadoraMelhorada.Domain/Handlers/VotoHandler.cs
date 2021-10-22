using LocadoraMelhorada.Domain.Commands.Inputs.Voto;
using LocadoraMelhorada.Domain.Commands.Outputs;
using LocadoraMelhorada.Domain.Entidades;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Handlers
{
    public class VotoHandler : ICommandHandler<AdicionarVotoCommand>, ICommandHandler<ExcluirVotoCommand>
    {
        private readonly IVotoRepository<long> _votoRepository;
        private readonly IFilmeRepository<long> _filmeRepository;
        private readonly IUsuarioRepository<long> _usuarioRepository;

        public VotoHandler(IVotoRepository<long> votoRepository, IFilmeRepository<long> filmeRepository, IUsuarioRepository<long> usuarioRepository)
        {
            _votoRepository = votoRepository;
            _filmeRepository = filmeRepository;
            _usuarioRepository = usuarioRepository;
        }

        public ICommandResult Handle(AdicionarVotoCommand command)
        {
            if (!command.ValidarCommand())
                return new VotoCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_filmeRepository.CheckId(command.FilmeId))
                return new VotoCommandResult(false, "Este filme não existe", command.Notifications);

            if (!_usuarioRepository.CheckId(command.UsuarioId))
                return new VotoCommandResult(false, "Este usuário não existe", command.Notifications);

            if (_votoRepository.JaFoiVotado(command.UsuarioId, command.FilmeId))
                return new VotoCommandResult(false, "O usuário já votou nesse filme", command.Notifications);

            Voto voto = new Voto(command.UsuarioId, command.FilmeId);

            var id = _votoRepository.Inserir(voto);

            voto.AtualizarId(id);

            return new VotoCommandResult(true, "Voto computado com sucesso!", voto);
        }

        public ICommandResult Handle(ExcluirVotoCommand command)
        {
            if (!command.ValidarCommand())
                return new VotoCommandResult(false, "Possui erros de validação", command.Notifications);

            if (!_votoRepository.CheckId(command.Id))
                return new VotoCommandResult(false, "Este voto não existe", command.Notifications);

            if (!_filmeRepository.CheckId(command.Id))
                return new VotoCommandResult(false, "Este filme não existe", command.Notifications);

            if (!_usuarioRepository.CheckId(command.Id))
                return new VotoCommandResult(false, "Este usuário não existe", command.Notifications);

            _votoRepository.Excluir(command.Id);

            return new VotoCommandResult(true, "Voto excluído com sucesso!", new
            {
                Id = command.Id
            });
        }
    }
}
