using System;
using System.Collections.Generic;

namespace Pontinho.Domain
{
    public class Match : AbstractTrackedPersistentEntity
    {
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public DateTime Date { get; set; }
        public virtual Player Winner { get; set; }
        public virtual List<Round> Rounds { get; set; } = new List<Round>();
    }
}