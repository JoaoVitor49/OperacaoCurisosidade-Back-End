using Projeto.Utils;

namespace Projeto.Features.Log.Queries
{
    public class GetAllLogsQuery : PaginatedQuery
    {
        public string? SearchTerm { get; set; }
    }
}
