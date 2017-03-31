using System;

namespace Pontinho.Domain
{
    public abstract class AbstractTrackedPersistentEntity : ITrackedPersistentEntity
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    public interface ITrackedPersistentEntity
    {
        int Id { get; set; }
        DateTime CreatedUtc { get; set; }
        DateTime ModifiedUtc { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}