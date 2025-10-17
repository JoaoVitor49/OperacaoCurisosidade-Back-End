namespace Projeto.Features.Client.Commands
{
    public class UpdateClientCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Others { get; set; } = string.Empty;
        public string Interests { get; set; } = string.Empty;
        public string Feelings { get; set; } = string.Empty;
        public string Values { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
