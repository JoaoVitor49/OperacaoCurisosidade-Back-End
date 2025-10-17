using Projeto.Utils;

namespace Projeto.Features.Client.Queries
{
    public class GetTotalClientQuery : SortQuery
    {
        public string? SearchTerm { get; set; }
    }
}
