using Projeto.Features.Client;
using Projeto.Features.Client.Dtos;
using Projeto.Features.Client.Queries;

namespace Projeto.Repositories
{
    public interface IClientRepository
    {
        Task<(List<ClientDashboardDTO>, int)> GetAllClientsAsync(GetAllClientQuery query, int? userId = null);
        Task<(List<ClientDashboardDTO>, int)> GetTotalClientsAsync(GetTotalClientQuery query, int? userId = null);
        Task<(List<ClientDashboardDTO>, int)> GetInactivesClientsAsync(GetInactiveClientQuery query, int? userId = null);
        Task<(List<ClientDashboardDTO>, int)> GetLastMonthClientsAsync(GetLastMonthClientQuery query, int? userId = null);
        Task<List<ClientDashboardDTO>> PrintClientsAsync(int? userId = null);
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client?> GetClientByEmailAsync(string email);
        Task<Client> AddClientAsync(Client client);
        Task SaveChangesAsync();
        Task<Client?> DeleteClientAsync(int id);
    }
}
