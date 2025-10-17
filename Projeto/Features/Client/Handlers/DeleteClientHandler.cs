using Projeto.Exceptions;
using Projeto.Features.Client.Commands;
using Projeto.Repositories;
using Projeto.Service;
using System.Security.Claims;

namespace Projeto.Features.Client.Handlers
{
    public class DeleteClientHandler
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogService _logService;

        public DeleteClientHandler(IClientRepository clientRepository, ILogService logService)
        {
            _clientRepository = clientRepository;
            _logService = logService;
        }

        public async Task Handle(DeleteClientCommand command, ClaimsPrincipal user)
        {
            var client = await _clientRepository.DeleteClientAsync(command.Id) 
                ?? throw new NotFoundException("Client not found.");
            var emailUser = user.FindFirst(ClaimTypes.Email)?.Value;
            await _logService.LogClientDeletedAsync(client.Email, emailUser);
        }
    }
}
