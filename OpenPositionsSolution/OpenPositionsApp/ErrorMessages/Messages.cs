using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.ErrorMessages
{
    [ExcludeFromCodeCoverage]
    public class Messages
    {
        public List<string> messages = new List<string>();
        public Messages()
        {
            messages = new List<string>() {
                "Currently we are working with the database!! try again later",
                "Invalid Location",
                "Unable to remove position at this moment",
                "Can't update at this time",
                "Positions not available",
                "Locations not available",
                "Can't Filter",
                "unable to add at this time",
                "Successfully Added"
            };
        }
    }
}
