namespace Projeto.Utils
{
    public abstract class PaginatedQuery
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
