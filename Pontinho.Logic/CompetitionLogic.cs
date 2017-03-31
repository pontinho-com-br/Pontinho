using System;
using System.Collections.Generic;
using System.Linq;
using Pontinho.Data;
using Pontinho.Domain;
using Pontinho.Dto;
using Pontinho.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Pontinho.Logic
{
    public class CompetitionLogic : ICompetitionLogic
    {
        private readonly PontinhoDbContext _dbContext;

        public CompetitionLogic(PontinhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CompetitionDto> Get(ApplicationUser user)
        {
            return GetEntities(user).Select(Project).ToList();
        }

        public Competition GetEntity(ApplicationUser user, int id)
        {
            var entity = GetEntities(user).FirstOrDefault(c => c.Id == id);
            if (entity == null) throw new UnauthorizedAccessException("Competition does NOT exist or you do NOT have permission to access it");
            return entity;
        }

        public IQueryable<Competition> GetEntities(ApplicationUser user)
        {
            return _dbContext.Competitions.Where(c => c.CreatedBy == user.UserName || c.Players.Any(p => p.UserId == user.Id)).Include(c => c.Matches).Include(c => c.Players);
        }

        public CompetitionDto Get(ApplicationUser user, int id)
        {
            var entity = GetEntity(user, id);
            return Project(entity);
        }

        public CompetitionDto Post(ApplicationUser user, CompetitionDto model)
        {
            var entity = model.Id > 0 ? GetEntity(user, model.Id) : new Competition();
            Bind(model, entity);
            if (model.Id > 0)
                _dbContext.Competitions.Update(entity);
            else
                _dbContext.Competitions.Add(entity);
            _dbContext.SaveChanges();
            return Project(entity);
        }

        private void Bind(CompetitionDto model, Competition entity)
        {
            var winner = _dbContext.Players.Find(model.Winner?.Id ?? 0);

            entity.Players.Clear();
            foreach (var playerDto in model.Players)
            {
                var p = _dbContext.Players.Find(playerDto.Id);
                if (p != null)
                    entity.Players.Add(p);
            }

            entity.Name = model.Name;
            entity.Start = model.Start;
            entity.End = model.End;
            entity.JoiningFee = model.JoiningFee;
            entity.ReturnFee = model.ReturnFee;
            entity.Winner = winner;
            entity.MaxPoints = model.MaxPoints;
        }

        private static CompetitionDto Project(Competition entity)
        {
            var totalSpent = 0m;

            var entries = entity.Matches.SelectMany(m => m.Rounds).SelectMany(r => r.Players.Select(rp => $"{r.Match.Id}-{rp.Player.Id}")).Distinct().Count();
            var snaps = entity.Matches.SelectMany(m => m.Rounds.SelectMany(r => r.Players)).Where(p => p.Status == Status.Snap).Select(p => p.Id).Count();
            totalSpent = (entity.JoiningFee * entries) + (entity.ReturnFee * snaps);

            var players = entity.Matches.SelectMany(m => m.Rounds.SelectMany(r => r.Players.Select(p => new { p.Player, MatchId = m.Id })))
                .DistinctBy(p => new { p.Player.Id, p.MatchId })
                .GroupBy(m => m.Player).Select(g => new PlayerDto { Id = g.Key.Id, Name = g.Key.Name, TotalMatches = g.Count() }).ToList();

            var winners = entity.Matches.Where(m => m.Winner != null).GroupBy(m => m.Winner).Select(g => new PlayerDto { Id = g.Key.Id, Name = g.Key.Name, MatchesWon = g.Count() });

            var playersWinners = (from p in players
                                  join w in winners on p.Id equals w.Id
                                  select new PlayerDto { Id = p.Id, Name = p.Name, TotalMatches = p.TotalMatches, MatchesWon = w.MatchesWon }).ToList();

            return new CompetitionDto
            {
                Id = entity.Id,
                Start = entity.Start,
                Name = entity.Name,
                End = entity.End,
                JoiningFee = entity.JoiningFee,
                ReturnFee = entity.ReturnFee,
                Winner = entity.Winner != null ? new PlayerDto { Id = entity.Winner.Id, Name = entity.Winner.Name } : null,
                Matches = entity.Matches.Select(GameLogic.ProjectMatch).OrderByDescending(d => d.Date),
                InProgressMatches = entity.Matches.Where(m => m.Winner == null).Select(GameLogic.ProjectMatch),
                MaxPoints = entity.MaxPoints,
                Winners = playersWinners.OrderByDescending(p => Convert.ToInt32(p.MatchesWon / p.TotalMatches)),//entity.Matches.Where(m => m.Winner != null).GroupBy(m => m.Winner).Select(g => new PlayerDto { Id = g.Key.Id, Name = g.Key.Name, MatchesWon = g.Count() }).OrderByDescending(p => p.MatchesWon),
                TotalSpent = totalSpent,
                Players = entity.Players.Select(PlayerLogic.Project)
            };
        }
    }


    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}