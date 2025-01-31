namespace NpvCalculator.Core.Common
{
    public class ValidationErrorResponse
    {
        public required string Message { get; set; }
        public required List<ValidationError> Errors { get; set; }
    }
}
