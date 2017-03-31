using System;
using System.Collections.Generic;
using System.Linq;
using Pontinho.Data;
using Pontinho.Domain;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Logic
{
    public class MessageLogic : IMessageLogic
    {
        private readonly IPlayerLogic _PlayerLogic;

        public MessageLogic(IPlayerLogic PlayerLogic)
        {
            _PlayerLogic = PlayerLogic;
        }

        public IEnumerable<Message> GetMessages(ApplicationUser user)
        {
            return _PlayerLogic.GetMeEntity(user).Where(p => p.User == null).Select(Projection);
        }

        private static Message Projection(Player arg)
        {
            return new Message
            {
                Link = "share.list",
                Sender = arg.CreatedBy,
                Text = "Says to be playing with you"
            };
        }
    }
}