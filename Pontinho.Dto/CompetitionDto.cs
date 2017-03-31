using System;
using System.Collections.Generic;
using Pontinho.Domain;

namespace Pontinho.Dto
{
    public class CompetitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual PlayerDto Winner { get; set; }
        public decimal JoiningFee { get; set; }
        public decimal ReturnFee { get; set; }
        public int MaxPoints { get; set; }

        public virtual IEnumerable<MatchDto> Matches { get; set; }
        public virtual IEnumerable<PlayerDto> Players { get; set; }
        public virtual IEnumerable<MatchDto> InProgressMatches { get; set; }
        public virtual IEnumerable<PlayerDto> Winners { get; set; } 
        public decimal TotalSpent { get; set; }
        public bool IsInProgress => Winner == null;
    }
}