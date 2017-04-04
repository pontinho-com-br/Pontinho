using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pontinho.Dto;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CompetitionController : Controller
    {
        private readonly ICompetitionLogic _competitionLogic;
        private readonly IGameLogic _gameLogic;
        private readonly CurrentUserService _currentUserService;

        public CompetitionController(ICompetitionLogic competitionLogic, IGameLogic gameLogic, CurrentUserService currentUserService)
        {
            _competitionLogic = competitionLogic;
            _gameLogic = gameLogic;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public IEnumerable<CompetitionDto> Get()
        {
            return _competitionLogic.Get(_currentUserService.CurrentUser).Where(c => c.IsInProgress);
        }

        [HttpGet("{id}")]
        public CompetitionDto Get(int id)
        {
            return _competitionLogic.Get(_currentUserService.CurrentUser, id);
        }

        [HttpPost]
        public CompetitionDto Post([FromBody]CompetitionDto value)
        {
            return _competitionLogic.Post(_currentUserService.CurrentUser, value);
        }

        [Route("dashboard")]
        public DashboardDto GetDashboard()
        {
            return _gameLogic.GetDashboard(_currentUserService.CurrentUser);
        }
    }
}