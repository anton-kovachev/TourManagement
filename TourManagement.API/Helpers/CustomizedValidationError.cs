namespace TourManagement.API.Helpers
{
    public class CustomizedValidationError
    {
        public string ValidatorKey { get; }

        public string Message { get; }

        public CustomizedValidationError(string message, string validatorKey = "")
        {
            ValidatorKey = validatorKey;
            Message = message;
        }
    }
}