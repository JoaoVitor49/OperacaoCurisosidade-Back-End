namespace Projeto.Features.Log.Dtos
{
    public class LogResponseDTO
    {
        public int Id { get; set; }
        public string  UserEmail { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DateTime TimeDate { get; set; }
        public object? OldData { get; set; }
        public object? NewData { get; set; }
    }
}
