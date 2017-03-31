namespace Pontinho.Domain
{
    public class RoundPlayerStats: AbstractTrackedPersistentEntity
    {
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public Status Status { get; set; }
        public int PointsLost { get; set; }
        public int CurrentScore { get; set; }
        public int Order { get; set; }
        public int RoundId { get; set; }
        public virtual Round Round { get; set; }
    }
}