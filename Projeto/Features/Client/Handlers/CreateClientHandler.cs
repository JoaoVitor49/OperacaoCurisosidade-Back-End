using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.Client.Commands;
using Projeto.Features.Client.Dtos;
using Projeto.Repositories;
using Projeto.Service;
using System.Security.Claims;

namespace Projeto.Features.Client.Handlers
{
    public class CreateClientHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public CreateClientHandler(IClientRepository clientRepository, IMapper mapper, ILogService logService)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<ClientResponseDTO> Handle(CreateClientCommand command, ClaimsPrincipal user)
        {
            if(Utils.Utils.ValidateClient(command).Count > 0)
                throw new ValidationException("Validation failed: " + 
                    string.Join(", ", Utils.Utils.ValidateClient(command)));

            var existingClient = await _clientRepository.GetClientByEmailAsync(command.Email);
            if (existingClient != null)
                throw new EmailAlreadyExistsException();

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdClaim);

            var newClient = new Client
            {
                Name = command.Name,
                Age = command.Age,
                Email = command.Email,
                Address = command.Address,
                Others = command.Others,
                Interests = command.Interests,
                Feelings = command.Feelings,
                Values = command.Values,
                IsActive = command.IsActive,
                UserId = userId,
                RegisterDate = DateOnly.FromDateTime(DateTime.Now)
            };

            var createdClient = await _clientRepository.AddClientAsync(newClient);

            var emailUser = user.FindFirst(ClaimTypes.Email)?.Value;
            await _logService.LogClientCreatedAsync(newClient.Email, emailUser);

            return _mapper.Map<ClientResponseDTO>(createdClient);
        }
    }
}
