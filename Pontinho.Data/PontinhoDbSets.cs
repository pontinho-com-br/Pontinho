using Pontinho.Domain;
using Microsoft.EntityFrameworkCore;

namespace Pontinho.Data
{
    public partial class PontinhoDbContext
    {
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundPlayerStats> RoundPlayerStats { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}