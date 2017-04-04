using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pontinho.Domain;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageLogic _messageLogic;
        private readonly CurrentUserService _currentUserService;

        public MessageController(IMessageLogic messageLogic, CurrentUserService currentUserService)
        {
            _messageLogic = messageLogic;
            _currentUserService = currentUserService;
        }

        // GET: api/Message
        public IEnumerable<Message> Get()
        {
            return _messageLogic.GetMessages(_currentUserService.CurrentUser);
        }
    }
}