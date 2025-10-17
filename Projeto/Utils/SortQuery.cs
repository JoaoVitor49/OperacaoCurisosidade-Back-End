namespace Projeto.Utils
{
    public abstract class SortQuery : PaginatedQuery
    {
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
}
