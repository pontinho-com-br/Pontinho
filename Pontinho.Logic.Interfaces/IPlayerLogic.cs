using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Pontinho.Domain;
using Pontinho.Dto;

namespace Pontinho.Logic.Interfaces
{
    public interface IPlayerLogic
    {
        IEnumerable<PlayerDto> Get(ApplicationUser user);
        IEnumerable<PlayerDto> GetForCompetition(ApplicationUser user, int id);
        IEnumerable<PlayerDto> GetMe(ApplicationUser user);
        IQueryable<Player> GetMeEntity(ApplicationUser user);
        PlayerDto Get(ApplicationUser user, int id);
        void Delete(ApplicationUser user, int id);
        PlayerDto Update(ApplicationUser user, PlayerDto model);
        PlayerDto SetAsMe(ApplicationUser user, int id);
    }
}