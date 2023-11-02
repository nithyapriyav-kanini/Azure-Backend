using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InvalidLocationException : Exception
    {
        string message;
        public InvalidLocationException()
        {
            message = "Location not available";
        }
        public InvalidLocationException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
