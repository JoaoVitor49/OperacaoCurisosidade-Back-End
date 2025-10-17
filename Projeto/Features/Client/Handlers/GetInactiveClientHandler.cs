using Projeto.Features.Client.Dtos;
using Projeto.Features.Client.Queries;
using Projeto.Repositories;
using System.Security.Claims;

namespace Projeto.Features.Client.Handlers
{
    public class GetInactiveClientHandler
    {
        private readonly IClientRepository _clientRepository;

        public GetInactiveClientHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientListResultDTO> Handle(GetInactiveClientQuery query, ClaimsPrincipal user)
        {
            if (query.Page <= 0) query.Page = 1;
            if (query.Limit <= 0) query.Limit = 10;

            var role = user.FindFirstValue(ClaimTypes.Role);
            int? userId = role == "User" ? int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)) : null;

            var (clients, total) = await _clientRepository.GetInactivesClientsAsync(query, userId);
            return new ClientListResultDTO
            {
                Total = total,
                Clients = clients
            };
        }
    }
}
