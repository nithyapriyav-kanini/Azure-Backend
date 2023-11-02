using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Models.Context
{
    [ExcludeFromCodeCoverage]
    public class OpenPositionContext : DbContext
    {
        public OpenPositionContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<OpenPosition>? OpenPositions { get; set; }
    }
}
