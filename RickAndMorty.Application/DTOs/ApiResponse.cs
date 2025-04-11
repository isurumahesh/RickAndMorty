namespace RickAndMorty.Application.DTOs
{
    public class ApiResponse<T> where T : class
    {
        public PageInfo Info { get; set; }
        public List<T> Results { get; set; } = new();
    }
}