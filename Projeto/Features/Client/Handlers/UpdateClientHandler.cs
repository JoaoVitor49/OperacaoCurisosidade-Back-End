using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.Client.Commands;
using Projeto.Features.Client.Dtos;
using Projeto.Repositories;
using Projeto.Service;
using System.Security.Claims;

namespace Projeto.Features.Client.Handlers
{
    public class UpdateClientHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public UpdateClientHandler(IClientRepository clientRepository, IMapper mapper, ILogService logService)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<ClientResponseDTO> Handle(UpdateClientCommand command, ClaimsPrincipal user)
        {
            var client = await _clientRepository.GetClientByIdAsync(command.Id)
                ?? throw new NotFoundException("Client not found.");

            if(Utils.Utils.ValidateClient(command).Count > 0)
                throw new ValidationException("Validation failed: " + 
                    string.Join(", ", Utils.Utils.ValidateClient(command)));

            var existingClient = await _clientRepository.GetClientByEmailAsync(command.Email);
            if (existingClient != null && existingClient.Id != command.Id)
                throw new EmailAlreadyExistsException();

            var oldData = new ClientAuditDTO
            {
                Name = client.Name,
                Age =  client.Age,
                Email = client.Email,
                Address = client.Address,
                Others = client.Others,
                Interests = client.Interests,
                Feelings = client.Feelings,
                Values = client.Values,
                IsActive = client.IsActive
            };

            client.Name = command.Name;
            client.Age = command.Age;
            client.Email = command.Email;
            client.Address = command.Address;
            client.Others = command.Others;
            client.Interests = command.Interests;
            client.Feelings = command.Feelings;
            client.Values = command.Values;
            client.IsActive = command.IsActive;
            
            await _clientRepository.SaveChangesAsync();
            var newData = _mapper.Map<ClientAuditDTO>(client);

            var emailUser = user.FindFirst(ClaimTypes.Email)?.Value;
            await _logService.LogClientUpdatedAsync(client.Email, oldData, newData, emailUser);

            return _mapper.Map<ClientResponseDTO>(client);
        }
    }
}
