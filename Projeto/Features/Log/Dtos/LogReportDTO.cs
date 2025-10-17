namespace Projeto.Features.Log.Dtos
{
    public class LogReportDTO
    {
        public int Id { get; set; }
        public DateTime TimeDate { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
