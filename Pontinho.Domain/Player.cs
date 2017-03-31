using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Pontinho.Domain
{
    public class Player : AbstractTrackedPersistentEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}