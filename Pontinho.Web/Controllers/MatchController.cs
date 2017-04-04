using Microsoft.AspNetCore.Mvc;
using Pontinho.Dto;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly CurrentUserService _currentUserService;

        public MatchController(IGameLogic gameLogic, CurrentUserService currentUserService)
        {
            _gameLogic = gameLogic;
            _currentUserService = currentUserService;
        }

        // GET: api/Match/5
        public MatchDto Get(int id)
        {
            return _gameLogic.GetMatch(_currentUserService.CurrentUser, id);
        }

        // DELETE: api/Match/5
        [HttpGet]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            _gameLogic.Delete(_currentUserService.CurrentUser, id);
        }
    }
}