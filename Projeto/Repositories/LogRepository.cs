using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Features.Log;
using Projeto.Features.Log.Dtos;
using Projeto.Features.Log.Queries;

namespace Projeto.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<LogReportDTO>, int)> GetAllLogsAsync(GetAllLogsQuery query, string? emailLogado = null)
        {
            var totalRecords = new SqlParameter
            {
                ParameterName = "@TotalRecords",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            var sql = "EXEC LogList @Search={0}, @Page={1}, @Limit={2}, @EmailLogado={3}, @TotalRecords={4} OUTPUT";

            var logs = await _context.LogsReport
                .FromSqlRaw(sql, query.SearchTerm, query.Page, query.Limit, emailLogado, totalRecords)
                .ToListAsync();

            var total = (int)(totalRecords.Value ?? 0);

            return (logs, total);
        }

        public async Task<Log?> GetLogByIdAsync(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null || log.Removed)
                return null;
            return log;
        }

        public async Task<Log> AddLogAsync(Log log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}
