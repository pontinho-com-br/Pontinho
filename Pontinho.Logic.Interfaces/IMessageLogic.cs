using System.Collections.Generic;
using Pontinho.Domain;

namespace Pontinho.Logic.Interfaces
{
    public interface IMessageLogic
    {
        IEnumerable<Message> GetMessages(ApplicationUser user);
    }
}