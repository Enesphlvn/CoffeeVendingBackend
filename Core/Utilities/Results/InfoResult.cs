namespace Core.Utilities.Results
{
    public class InfoResult : IResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string InfoMessage { get; }

        public InfoResult(string message, string infoMessage)
        {
            Success = true;
            Message = message;
            InfoMessage = infoMessage;
        }
    }
}
