using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;
using System.Text.Json.Serialization;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Filme
{
    public class AtualizarFilmeCommand : Notifiable<Notification>, ICommandPadrao
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string Titulo { get; set; }

        public string Diretor { get; set; }

        public bool ValidarCommand()
        {
            if (Id == 0)
                AddNotification("Id", "Id precisa ser maior que 0");

            if (string.IsNullOrWhiteSpace(Titulo))
                AddNotification("Titulo", "Titulo é um campo obrigatório");
            else if (Titulo.Length > 100)
                AddNotification("Titulo", "Titulo maior que 100 caracteres");

            if (string.IsNullOrWhiteSpace(Diretor))
                AddNotification("Diretor", "Diretor é um campo obrigatório");
            else if (Diretor.Length > 100)
                AddNotification("Diretor", "Diretor maior que 100 caracteres");

            return IsValid;
        }
    }
}
