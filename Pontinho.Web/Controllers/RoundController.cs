using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pontinho.Dto;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RoundController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly CurrentUserService _currentUserService;

        public RoundController(IGameLogic gameLogic, CurrentUserService currentUserService)
        {
            _gameLogic = gameLogic;
            _currentUserService = currentUserService;
        }

        // POST: api/Round
        public MatchDto Post([FromBody]RoundDto value)
        {
            return _gameLogic.PostRound(_currentUserService.CurrentUser, value);
        }

        // DELETE: api/Match/5
        [HttpGet]
        [Route("deleteLastFromMatch/{id}")]
        public void Delete(int id)
        {
            _gameLogic.DeleteLastRound(_currentUserService.CurrentUser, id);
        }
    }
}