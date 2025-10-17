using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.Log.Commands;
using Projeto.Features.Log.Dtos;
using Projeto.Repositories;

namespace Projeto.Features.Log.Handlers
{
    public class CreateLogHandler
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public CreateLogHandler(ILogRepository logRepository, IMapper mapper) 
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<LogResponseDTO> Handle(CreateLogCommand command)
        {
            if(Utils.Utils.ValidateLog(command).Count > 0)
                throw new ValidationException("Validation failed: " 
                    + string.Join(", ", Utils.Utils.ValidateLog(command)));

            var newLog = new Log()
            {
                TimeDate = DateTime.Now,
                UserEmail = command.User,
                Action = command.Action,
                OldData = command.OldData,
                NewData = command.NewData,
            };

            var log = await _logRepository.AddLogAsync(newLog);
            return _mapper.Map<LogResponseDTO>(log);
        }
    }
}
