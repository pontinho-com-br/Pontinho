using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pontinho.Dto;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerLogic _playerLogic;
        private readonly CurrentUserService _currentUserService;
        private readonly ICompetitionLogic _competitionLogic;

        public PlayerController(IPlayerLogic playerLogic, CurrentUserService currentUserService, ICompetitionLogic competitionLogic)
        {
            _playerLogic = playerLogic;
            _currentUserService = currentUserService;
            _competitionLogic = competitionLogic;
        }

        // GET: api/PlayerApi
        public IEnumerable<PlayerDto> Get()
        {
            return _playerLogic.Get(_currentUserService.CurrentUser);
        }

        [Route("competition/{id}")]
        public IEnumerable<PlayerDto> GetForCompetition(int id)
        {
            return _playerLogic.GetForCompetition(_currentUserService.CurrentUser, id);
        }

        [Route("me")]
        public IEnumerable<PlayerDto> GetMe()
        {
            return _playerLogic.GetMe(_currentUserService.CurrentUser);
        }

        public PlayerDto Get(int id)
        {
            return _playerLogic.Get(_currentUserService.CurrentUser, id);
        }

        // POST: api/PlayerApi
        public PlayerDto Post([FromBody]PlayerDto value)
        {
            return _playerLogic.Update(_currentUserService.CurrentUser, value);
        }

        [HttpPost]
        [Route("setme/{id}")]
        public PlayerDto SetPlayerAsMe(int id)
        {
            return _playerLogic.SetAsMe(_currentUserService.CurrentUser, id);
        }

        // DELETE: api/Match/5
        [HttpGet]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            _playerLogic.Delete(_currentUserService.CurrentUser, id);
        }
    }
}