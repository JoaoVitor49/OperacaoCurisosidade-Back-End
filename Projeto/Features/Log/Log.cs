using Projeto.Features.Client.Dtos;

namespace Projeto.Features.Log
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime TimeDate { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public ClientAuditDTO? OldData { get; set; }
        public ClientAuditDTO? NewData { get; set; }
        public bool Removed { get; set; } = false;
    }
}
