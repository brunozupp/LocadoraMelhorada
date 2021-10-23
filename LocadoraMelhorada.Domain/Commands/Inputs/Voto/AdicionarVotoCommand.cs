using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Voto
{
    public class AdicionarVotoCommand : Notifiable<Notification>, ICommandPadrao
    {
        public string UsuarioId { get; set; }

        public string FilmeId { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(UsuarioId))
                AddNotification("UsuarioId", "UsuarioId precisa ser maior que 0");

            if (string.IsNullOrWhiteSpace(FilmeId))
                AddNotification("FilmeId", "FilmeId precisa ser maior que 0");

            return IsValid;
        }
    }
}
