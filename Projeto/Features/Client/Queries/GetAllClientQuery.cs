using Projeto.Utils;

namespace Projeto.Features.Client.Queries
{
    public class GetAllClientQuery : SortQuery
    {
        public string? SearchTerm { get; set; }
    }
}
