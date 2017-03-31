namespace Pontinho.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public int TotalCompetitions { get; set; }
        public int TotalMatches { get; set; }
        public int TotalRounds { get; set; }
        public int CompetitionsWon { get; set; }
        public int MatchesWon { get; set; }
        public int RoundsWon { get; set; }
        public bool Validated { get; set; }
    }
}