using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Models
{
    [ExcludeFromCodeCoverage]
    public class Location
    {
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
    }
}
