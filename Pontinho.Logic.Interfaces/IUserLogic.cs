using Pontinho.Domain;
using Pontinho.Dto;

namespace Pontinho.Logic.Interfaces
{
    public interface IUserLogic
    {
        ApplicationUser GetEntity(string username);
        UserDto Get(ApplicationUser user, string username);
        UserDto Update(ApplicationUser user, UserDto model);
    }
}