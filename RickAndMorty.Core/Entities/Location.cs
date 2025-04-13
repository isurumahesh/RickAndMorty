using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Core.Entities
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Dimension { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }

        public List<Character> OriginCharacters { get; set; }  
        public List<Character> LocationCharacters { get; set; }
    }
}