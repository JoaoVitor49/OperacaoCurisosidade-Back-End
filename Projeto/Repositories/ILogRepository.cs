using Projeto.Features.Log;
using Projeto.Features.Log.Dtos;
using Projeto.Features.Log.Queries;

namespace Projeto.Repositories
{
    public interface ILogRepository
    {
        Task<(List<LogReportDTO>, int)> GetAllLogsAsync(GetAllLogsQuery query, string? emailLogado = null);
        Task<Log?> GetLogByIdAsync(int id);
        Task<Log> AddLogAsync(Log log);
    }
}