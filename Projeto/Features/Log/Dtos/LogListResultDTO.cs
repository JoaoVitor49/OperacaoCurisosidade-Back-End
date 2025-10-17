namespace Projeto.Features.Log.Dtos
{
    public class LogListResultDTO
    {
        public int Total { get; set; }
        public List<LogReportDTO>? Logs { get; set; }
    }
}
