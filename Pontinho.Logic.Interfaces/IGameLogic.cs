using Pontinho.Domain;
using Pontinho.Dto;

namespace Pontinho.Logic.Interfaces
{
    public interface IGameLogic
    {
        MatchDto GetMatch(ApplicationUser user, int id);

        MatchDto PostRound(ApplicationUser user, RoundDto model);

        DashboardDto GetDashboard(ApplicationUser user);

        void Delete(ApplicationUser user, int id);

        void DeleteLastRound(ApplicationUser user, int id);
    }
}