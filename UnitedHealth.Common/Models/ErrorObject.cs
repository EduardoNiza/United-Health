namespace UnitedHealth.Common.Models
{
    public class ErrorObject
    {
        public ErrorObject(string message)
        {
            Message = message;
        }

        public string Message { get; } 
    }
}
