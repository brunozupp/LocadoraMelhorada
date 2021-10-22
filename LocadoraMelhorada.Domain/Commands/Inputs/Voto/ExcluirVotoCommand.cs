using Flunt.Notifications;
using LocadoraMelhorada.Infra.Interfaces.Commands;
using System.Text.Json.Serialization;

namespace LocadoraMelhorada.Domain.Commands.Inputs.Voto
{
    public class ExcluirVotoCommand : Notifiable<Notification>, ICommandPadrao
    {
        [JsonIgnore]
        public long Id { get; set; }

        public bool ValidarCommand()
        {
            if (Id <= 0)
                AddNotification("Id", "Id precisa ser maior que 0");


            return IsValid;
        }
    }
}
