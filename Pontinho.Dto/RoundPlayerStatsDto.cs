using Pontinho.Domain;

namespace Pontinho.Dto
{
    public class RoundPlayerStatsDto
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int PlayerId { get; set; }
        public int Order { get; set; }
        public Status Status { get; set; }
        public int PointsLost { get; set; }
        public int CurrentScore { get; set; }
    }
}