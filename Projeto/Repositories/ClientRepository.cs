using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Features.Client;
using Projeto.Features.Client.Dtos;
using Projeto.Features.Client.Queries;

namespace Projeto.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<ClientDashboardDTO>, int)> GetAllClientsAsync(GetAllClientQuery query, int? userId = null)
        {
            var queryable = _context.DashboardClients
                .AsNoTracking()
                .AsQueryable();

            if (userId.HasValue)
                queryable = queryable.Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(query.SearchTerm))
                queryable = queryable.Where(c => c.Name.Contains(query.SearchTerm) || c.Email.Contains(query.SearchTerm));

            if (!string.IsNullOrEmpty(query.SortBy))
                queryable = query.SortBy.ToLower() switch
                {
                    "name" => query.SortDescending ? queryable.OrderByDescending(c => c.Name) : queryable.OrderBy(c => c.Name),
                    "email" => query.SortDescending ? queryable.OrderByDescending(c => c.Email) : queryable.OrderBy(c => c.Email),
                    "status" => query.SortDescending ? queryable.OrderByDescending(c => c.IsActive) : queryable.OrderBy(c => c.IsActive),
                    _ => queryable
                };

            var total = await queryable.CountAsync();

            var clients = await queryable
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return (clients, total);
        }

        public async Task<(List<ClientDashboardDTO>, int)> GetTotalClientsAsync(GetTotalClientQuery query, int? userId = null)
        {
            var queryable = _context.DashboardClients
                .AsNoTracking()
                .OrderByDescending(c => c.RegisterDate)
                .AsQueryable();

            if(userId.HasValue)
                queryable = queryable.Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(query.SearchTerm))
                queryable = Utils.Utils.SearchFilterQueryable(queryable, query.SearchTerm);

            if (!string.IsNullOrEmpty(query.SortBy))
                queryable = Utils.Utils.SortByQueryable(queryable, query.SortBy, query.SortDescending);

            var total = await queryable.CountAsync();

            var clients = await queryable
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return (clients, total);
        }

        public async Task<(List<ClientDashboardDTO>, int)> GetInactivesClientsAsync(GetInactiveClientQuery query, int? userId = null)
        {
            var queryable = _context.DashboardClients
                .AsNoTracking()
                .OrderByDescending(c => c.RegisterDate)
                .Where(c => !c.IsActive)
                .AsQueryable();

            if (userId.HasValue)
                queryable = queryable.Where(c => c.UserId == userId);

            var total = await queryable
                .AsNoTracking()
                .CountAsync();

            if (!string.IsNullOrEmpty(query.SortBy))
                queryable = Utils.Utils.SortByQueryable(queryable, query.SortBy, query.SortDescending);

            var clients = await queryable
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return (clients, total);
        }

        public async Task<(List<ClientDashboardDTO>, int)> GetLastMonthClientsAsync(GetLastMonthClientQuery query, int? userId = null)
        {
            var queryable = _context.DashboardClients
                .AsNoTracking()
                .Where(c => c.RegisterDate >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)))
                .OrderByDescending(c => c.RegisterDate)
                .AsQueryable();

            if (userId.HasValue)
                queryable = queryable.Where(c => c.UserId == userId);

            var total = await queryable.CountAsync();

            if (!string.IsNullOrEmpty(query.SortBy))
                queryable = Utils.Utils.SortByQueryable(queryable, query.SortBy, query.SortDescending);

            var clients = await queryable
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();
            
            return (clients, total);
        }        

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if(client == null || client.Removed)
                return null;
            return client;
        }

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == email && !c.Removed);
            return client;
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Client?> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null || client.Removed)
                return null;
            client.Removed = true;
            await _context.SaveChangesAsync();
            return client;
        }

        public Task<List<ClientDashboardDTO>> PrintClientsAsync(int? userId = null)
        {
            var queryable = _context.DashboardClients
                .AsNoTracking()
                .AsQueryable();

            if (userId.HasValue)
                queryable = queryable.Where(c => c.UserId == userId);

            var clients = queryable.ToListAsync();
            return clients;
        }
    }
}
