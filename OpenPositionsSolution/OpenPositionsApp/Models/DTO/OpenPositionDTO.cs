using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class OpenPositionDTO
    {
        public int PositionId { get; set; }
        public string? RoleName { get; set; }
        public string? Domain { get; set; }
        public string? JobDescription { get; set; }

        public string? Location { get; set; }
        public string? ReqSkills { get; set; }
        public string? EducationalQual { get; set; }
        public int? Experience { get; set; }
    }
}
