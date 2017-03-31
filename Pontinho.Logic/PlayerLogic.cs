using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Pontinho.Data;
using Pontinho.Domain;
using Pontinho.Dto;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Logic
{
    public class PlayerLogic : IPlayerLogic
    {
        private readonly PontinhoDbContext _dbContext;
        private readonly ICompetitionLogic _competitionLogic;

        public PlayerLogic(PontinhoDbContext dbContext, ICompetitionLogic competitionLogic)
        {
            _dbContext = dbContext;
            _competitionLogic = competitionLogic;
        }

        public IEnumerable<PlayerDto> Get(ApplicationUser user)
        {
            return _dbContext.Players.Where(p => p.CreatedBy == user.UserName).Select(Project).ToList();
        }

        public IEnumerable<PlayerDto> GetForCompetition(ApplicationUser user, int id)
        {
            var comp = _competitionLogic.GetEntity(user, id);
            return comp.Players.Select(Project);
        }

        public IEnumerable<PlayerDto> GetMe(ApplicationUser user)
        {
            return GetMeEntity(user).Select(Project);
        }

        public IQueryable<Player> GetMeEntity(ApplicationUser user)
        {
            return _dbContext.Players.Where(p => p.Email.ToLower() == user.Email.ToLower());
        }

        public PlayerDto Get(ApplicationUser user, int id)
        {
            var dto = _dbContext.Players.Where(p => p.Id == id)
                .Select(Project).FirstOrDefault();

            if (dto != null)
            {
                dto.CompetitionsWon = _dbContext.Competitions.Count(c => c.Winner.Id == id);
                dto.MatchesWon = _dbContext.Matches.Count(c => c.Winner.Id == id);
                dto.RoundsWon = _dbContext.RoundPlayerStats.Count(c => c.Player.Id == id && c.Status == Status.Win);
            }
            return dto;
        }

        public void Delete(ApplicationUser user, int id)
        {
            var entity = _dbContext.Players.FirstOrDefault(p => p.CreatedBy == user.UserName && p.Id == id);
            if (entity == null) throw new UnauthorizedAccessException("Player does NOT exist or you do NOT have permission to delete it");
            //var matches = _dbContext.Matches.Where(m => m.Winner.Id == id).ToList();
            //foreach (var match in matches)
            //{
            //    _dbContext.Matches.Remove(match);
            //}
            _dbContext.Players.Remove(entity);
            _dbContext.SaveChanges();
        }

        public PlayerDto Update(ApplicationUser user, PlayerDto model)
        {
            var entity = model.Id == 0 ? new Player() : _dbContext.Players.FirstOrDefault(p => p.CreatedBy == user.UserName && p.Id == model.Id);
            if (entity == null) throw new UnauthorizedAccessException("Player does NOT exist or you do NOT have permission to access it");
            BindToEntity(model, entity);
            _dbContext.Players.AddOrUpdate(entity);
            _dbContext.SaveChanges();
            return Project(entity);
        }

        public PlayerDto SetAsMe(ApplicationUser user, int id)
        {
            var entity = GetMeEntity(user).FirstOrDefault(p => p.Id == id);
            if (entity == null) throw new UnauthorizedAccessException("Player does NOT exist or you do NOT have permission to access it");
            entity.UserId = user.Id;
            _dbContext.SaveChanges();
            return Project(entity);
        }

        private void BindToEntity(PlayerDto model, Player entity)
        {
            entity.Email = model.Email;
            entity.Name = model.Name;
        }

        public static PlayerDto Project(Player entity)
        {
            return new PlayerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreatedBy = entity.CreatedBy,
                Validated = !string.IsNullOrWhiteSpace(entity.UserId)
            };
        }
    }
}