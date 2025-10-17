using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.Client.Dtos;
using Projeto.Features.Client.Queries;
using Projeto.Repositories;

namespace Projeto.Features.Client.Handlers
{
    public class GetClientByIdHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponseDTO> Handle (GetClientByIdQuery query)
        {
            var client = await _clientRepository.GetClientByIdAsync(query.Id)
                ?? throw new NotFoundException("Client not found.");
            return _mapper.Map<ClientResponseDTO>(client);
        }
    }
}
