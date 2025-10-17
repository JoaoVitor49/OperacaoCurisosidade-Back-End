using Projeto.Features.Client.Dtos;
using Projeto.Features.User;
using Projeto.Repositories;
using System.Security.Claims;

namespace Projeto.Features.Client.Handlers
{
    public class PrintClientHandler
    {
        private readonly IClientRepository _clientRepository;

        public PrintClientHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<List<ClientDashboardDTO>> Handle(ClaimsPrincipal user)
        {
            var role = user.FindFirstValue(ClaimTypes.Role);
            int? userId = role == "User" ? int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)) : null;
            var clients = _clientRepository.PrintClientsAsync(userId);
            return clients;
        }
    }
}
