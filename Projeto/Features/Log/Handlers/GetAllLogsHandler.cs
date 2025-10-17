using Projeto.Features.Log.Dtos;
using Projeto.Features.Log.Queries;
using Projeto.Repositories;
using System.Security.Claims;

namespace Projeto.Features.Log.Handlers
{
    public class GetAllLogsHandler
    {
        private readonly ILogRepository _logRepository;
        public GetAllLogsHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<LogListResultDTO> Handle(GetAllLogsQuery query, ClaimsPrincipal user)
        {
            if(query.Page <= 0) query.Page = 1;
            if(query.Limit <= 0) query.Limit = 10;

            var role = user.FindFirstValue(ClaimTypes.Role);
            var emailUser = role == "User" ? user.FindFirstValue(ClaimTypes.Email) : null;

            var (logs, total) = await _logRepository.GetAllLogsAsync(query, emailUser);
            return new LogListResultDTO
            {
                Total = total,
                Logs = logs
            };
        }
    }
}
