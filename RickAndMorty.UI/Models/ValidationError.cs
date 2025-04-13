namespace RickAndMorty.UI.Models
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string AttemptedValue { get; set; }
        public object CustomState { get; set; }
        public int Severity { get; set; }
        public string ErrorCode { get; set; }
    }
}
