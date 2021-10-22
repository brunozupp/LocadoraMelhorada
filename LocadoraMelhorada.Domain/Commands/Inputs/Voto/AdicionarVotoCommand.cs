using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Voto
{
    public class AdicionarVotoCommand : Notifiable<Notification>, ICommandPadrao
    {
        public long UsuarioId { get; set; }

        public long FilmeId { get; set; }

        public bool ValidarCommand()
        {
            if (UsuarioId <= 0)
                AddNotification("UsuarioId", "UsuarioId precisa ser maior que 0");

            if (FilmeId <= 0)
                AddNotification("FilmeId", "FilmeId precisa ser maior que 0");

            return IsValid;
        }
    }
}
