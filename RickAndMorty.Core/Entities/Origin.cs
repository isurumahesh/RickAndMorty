namespace RickAndMorty.Core.Entities
{
    public class Origin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}