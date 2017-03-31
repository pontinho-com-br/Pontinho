using System;
using System.Linq;
using Pontinho.Data;
using Pontinho.Domain;
using Pontinho.Dto;
using Pontinho.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pontinho.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly PontinhoDbContext _dbContext;

        public UserLogic(PontinhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ApplicationUser GetEntity(string username)
        {
            var user = _dbContext.Users
                .Include(s => s.Claims)
                .Include(s => s.Roles)
                .FirstOrDefault(u => u.UserName == username);

            return user;
        }

        public UserDto Get(ApplicationUser user, string username)
        {
            var entity = GetEntity(username);
            if (entity == null || entity.Id != user.Id) throw new UnauthorizedAccessException("User does NOT exist or you do NOT have access to it");
            return Project(entity);
        }

        public UserDto Update(ApplicationUser user, UserDto model)
        {
            var entity = GetEntity(model.Username);
            if (entity == null || entity.Id != user.Id) throw new UnauthorizedAccessException("User does NOT exist or you do NOT have access to it");
            BindToEntity(model, entity);
            _dbContext.SaveChanges();
            return Project(entity);
        }

        private void BindToEntity(UserDto model, ApplicationUser entity)
        {
            entity.Email = model.Email;
        }

        private UserDto Project(ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            };
        }
    }
}