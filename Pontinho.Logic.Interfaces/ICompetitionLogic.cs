using System.Collections.Generic;
using Pontinho.Domain;
using Pontinho.Dto;

namespace Pontinho.Logic.Interfaces
{
    public interface ICompetitionLogic
    {
        IEnumerable<CompetitionDto> Get(ApplicationUser user);
        Competition GetEntity(ApplicationUser user, int id);
        CompetitionDto Get(ApplicationUser user, int id);
        CompetitionDto Post(ApplicationUser user, CompetitionDto entity);
    }
}