using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Autenticacao
{
    public class AutenticacaoCommand : Notifiable<Notification>, ICommandPadrao
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrEmpty(Login))
                AddNotification("Login", "Login é obrigatório.");

            if (string.IsNullOrEmpty(Senha))
                AddNotification("Senha", "Senha é obrigatório.");

            return IsValid;
        }
    }
}
