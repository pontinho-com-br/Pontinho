using System.Collections.Generic;
using Pontinho.Domain;

namespace Pontinho.Dto
{
    public class RoundDto
    {
        public int Id { get; set; }
        public int RoundNo { get; set; }
        public int CompetitionId { get; set; }
        public int MatchId { get; set; }
        public virtual IEnumerable<RoundPlayerStatsDto> Players { get; set; }
        public int Carding { get; set; }
    }
}