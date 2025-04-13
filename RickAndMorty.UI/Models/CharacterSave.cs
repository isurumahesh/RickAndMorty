using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.UI.Models
{
    public class CharacterSave
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty;
        [Required(ErrorMessage = "Species is required")]
        public string Species { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;
    }
}
