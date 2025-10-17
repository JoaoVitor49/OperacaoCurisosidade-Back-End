namespace Projeto.Features.Client.Dtos
{
    public class ClientListResultDTO
    {
        public int Total { get; set; }
        public List<ClientDashboardDTO>? Clients { get; set; }
    }
}
