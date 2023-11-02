using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NullResultException : Exception
    {

        string message;
        public NullResultException()
        {
            message = "Result is null";
        }
        public NullResultException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
