namespace RickAndMorty.Application.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public OriginDTO Origin { get; set; }
        public LocationDTO Location { get; set; }
        public string Image { get; set; }
        public List<string> Episode { get; set; } = new();
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
}