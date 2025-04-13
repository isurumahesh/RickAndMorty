namespace RickAndMorty.UI.Models
{
    public class ValidationErrorResponse
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; }
        public List<string> RuleSetsExecuted { get; set; }
    }
}
