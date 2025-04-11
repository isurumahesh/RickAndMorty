namespace RickAndMorty.Core.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}