using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.Log.Dtos;
using Projeto.Features.Log.Queries;
using Projeto.Repositories;

namespace Projeto.Features.Log.Handlers
{
    public class GetLogByIdHandler
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;
        public GetLogByIdHandler(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }
        public async Task<LogResponseDTO> Handle(GetLogByIdQuery query)
        {
            var log = await _logRepository.GetLogByIdAsync(query.Id)
                ?? throw new NotFoundException("Log not found.");
            return _mapper.Map<LogResponseDTO>(log);
        }
    }
}
