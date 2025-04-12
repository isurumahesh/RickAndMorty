namespace RickAndMorty.UI.Models
{
    public class PaginatedList
    {
        public List<Character> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }

        public PaginatedList(List<Character> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
