namespace RickAndMorty.UI.Models
{
    public class ApiPaginatedResponse
    {
        public PaginationInfo Info { get; set; }
        public List<Character> Results { get; set; }
    }
}
