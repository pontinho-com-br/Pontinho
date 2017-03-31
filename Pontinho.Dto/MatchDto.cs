using System;
using System.Collections.Generic;

namespace Pontinho.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Competition { get; set; }
        public bool IsInProgress => Winner == null;
        public DateTime Date { get; set; }
        public virtual PlayerDto Winner { get; set; }
        public virtual List<RoundDto> Rounds { get; set; } = new List<RoundDto>();
        public virtual List<PlayerDto> Players { get; set; } = new List<PlayerDto>();
        public virtual List<PlayerDto> AllPlayers { get; set; } = new List<PlayerDto>();
        public int MaxPoints { get; set; } = 100;
    }
}