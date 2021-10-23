using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;
using System.Text.Json.Serialization;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Filme
{
    public class ExcluirFilmeCommand : Notifiable<Notification>, ICommandPadrao
    {
        [JsonIgnore]
        public string Id { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Id))
                AddNotification("Id", "Id precisa ser maior que 0");

            return IsValid;
        }
    }
}
