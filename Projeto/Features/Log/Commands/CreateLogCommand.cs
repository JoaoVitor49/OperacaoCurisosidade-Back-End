using Projeto.Features.Client.Dtos;

namespace Projeto.Features.Log.Commands
{
    public class CreateLogCommand
    {
        public string User { get; set; } = string.Empty;    
        public string Action { get; set; } = string.Empty;
        public ClientAuditDTO? OldData { get; set; }
        public ClientAuditDTO? NewData { get; set; }
    }
}
