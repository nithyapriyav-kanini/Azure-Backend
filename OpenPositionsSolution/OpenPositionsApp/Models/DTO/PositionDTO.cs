using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class PositionDTO
    {
        public int PositionId { get; set; }
        public string? RoleName { get; set; }
        public string? Domain { get; set; }
        public string? Location { get; set; }
    }
}
