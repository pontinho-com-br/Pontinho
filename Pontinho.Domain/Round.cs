using System.Collections.Generic;

namespace Pontinho.Domain
{
    public class Round : AbstractTrackedPersistentEntity
    {
        public int Order { get; set; }
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
        public virtual ICollection<RoundPlayerStats> Players { get; set; }
        public virtual Player Carding { get; set; }
    }
}