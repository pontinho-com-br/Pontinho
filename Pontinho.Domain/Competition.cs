using System;
using System.Collections;
using System.Collections.Generic;

namespace Pontinho.Domain
{
    public class Competition : AbstractTrackedPersistentEntity
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual Player Winner { get; set; }
        public decimal JoiningFee { get; set; }
        public decimal ReturnFee { get; set; }
        public int MaxPoints { get; set; } = 100;
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}