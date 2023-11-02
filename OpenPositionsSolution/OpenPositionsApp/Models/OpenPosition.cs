using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Models
{
    [ExcludeFromCodeCoverage]
    public class OpenPosition
    {

        [Key]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [MinLength(5,ErrorMessage = "Role name must be atleast five characters")]
        public string? RoleName { get; set; }

        [Required(ErrorMessage = "Domain is required")]
        public string? Domain { get; set; }

        [MinLength(15, ErrorMessage = "Job Description must be atleast fifteen characters")]
        public string? JobDescription { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        public Location? Locations { get; set; }

        [Required(ErrorMessage = "Required skills is required")]
        public string? ReqSkills { get; set; }

        [Required(ErrorMessage = "Educational qualification is required")]
        public string? EducationalQual { get; set; }

        [Range(1, 50, ErrorMessage = "Experience must be between 1 and 50")]
        public int? Experience { get; set; }

    }
}
