using System.ComponentModel.DataAnnotations.Schema;

namespace RickAndMorty.Core.Entities
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string? Type { get; set; }
        public string Gender { get; set; }
        public string? Image { get; set; }
        public string? EpisodeUrlsJson { get; set; }
        public string? Url { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public Location? Origin { get; set; }
        public int? OriginId { get; set; }

        public Location? Location { get; set; }
        public int? LocationId { get; set; }
    }
}