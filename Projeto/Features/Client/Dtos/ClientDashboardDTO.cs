namespace Projeto.Features.Client.Dtos
{
    public class ClientDashboardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly RegisterDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
