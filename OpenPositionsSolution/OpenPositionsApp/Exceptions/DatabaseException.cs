using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DatabaseException : Exception
    {
        string message;
        public DatabaseException()
        {
            message = "Currently we have problem with our database server!! Try again later!!";
        }
        public DatabaseException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
