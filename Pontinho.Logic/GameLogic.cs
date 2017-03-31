using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Pontinho.Data;
using Pontinho.Domain;
using Pontinho.Dto;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Logic
{
    public class GameLogic : IGameLogic
    {
        private readonly PontinhoDbContext _dbContext;

        public GameLogic(PontinhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MatchDto GetMatch(ApplicationUser user, int id)
        {
            var entity = GetMatchEntity(id);
            return ProjectMatchDetailed(entity);
        }

        private Match GetMatchEntity(int id)
        {
            var entity = _dbContext.Matches.Where(m => m.Id == id)
                .Include(m => m.Competition)
                .Include(m => m.Winner)
                .Include(m => m.Rounds).FirstOrDefault();
            return entity;
        }

        public MatchDto PostRound(ApplicationUser user, RoundDto model)
        {
            Match match;
            if (model.MatchId > 0)
                match = GetMatchEntity(model.MatchId);
            else
            {
                var comp = _dbContext.Competitions.Find(model.CompetitionId);
                match = new Match
                {
                    Competition = comp,
                    Date = DateTime.UtcNow
                };
            }
            Round round;
            if (model.Id > 0)
                round = match.Rounds.FirstOrDefault(r => r.Id == model.Id);
            else
            {

                round = new Round { Match = match, Carding = _dbContext.Players.Find(model.Players.FirstOrDefault()?.PlayerId ?? 0) };
                match.Rounds.Add(round);
            }
            BindRound(model, round);
            SetWinner(match);
            _dbContext.Matches.AddOrUpdate(match);
            _dbContext.Rounds.AddOrUpdate(round);

            _dbContext.SaveChanges();
            return ProjectMatchDetailed(match);
        }

        public DashboardDto GetDashboard(ApplicationUser user)
        {
            var players = _dbContext.Players.Where(p => p.UserId == user.Id).Select(p => p.Id);

            var competitions = _dbContext.Competitions.Count(c => c.Players.Any(p => players.Contains(p.Id)));
            var matchesPlayed = _dbContext.Matches.Count(m => m.Rounds.Any(r => r.Players.Any(p => players.Contains(p.PlayerId))));
            var matchesWon = _dbContext.Matches.Count(m => players.Contains(m.Winner.Id) && m.Rounds.Any(r => r.Players.Any(p => players.Contains(p.PlayerId))));
            var roundsPlayed = _dbContext.RoundPlayerStats.Count(p => players.Contains(p.PlayerId));
            var roundsWon = _dbContext.RoundPlayerStats.Count(p => players.Contains(p.PlayerId) && p.Status == Status.Win);
            var roundsSnapped = _dbContext.RoundPlayerStats.Count(p => players.Contains(p.PlayerId) && p.Status == Status.Snap);

            var dashboard = new DashboardDto
            {
                TotalCompetitions = competitions,
                TotalMatches = matchesPlayed,
                TotalRounds = roundsPlayed,
                MatchesWon = matchesWon,
                RoundsWon = roundsWon,
                RoundsSnapped = roundsSnapped
            };
            return dashboard;
        }

        public void Delete(ApplicationUser user, int id)
        {
            var entity = _dbContext.Matches.FirstOrDefault(m => m.Id == id && (m.CreatedBy == user.UserName || m.Competition.CreatedBy == user.UserName));
            if (entity == null) throw new UnauthorizedAccessException("Match does NOT exist or you do NOT have access to delete it");
            _dbContext.Matches.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteLastRound(ApplicationUser user, int id)
        {
            var entity = _dbContext.Matches.Include(m => m.Rounds).FirstOrDefault(m => m.Id == id && (m.CreatedBy == user.UserName || m.Competition.CreatedBy == user.UserName));
            if (entity == null) throw new UnauthorizedAccessException("Match does NOT exist or you do NOT have access to it");
            if (entity.Winner != null) throw new UnauthorizedAccessException("Round cannot be deleted after match is over");
            var round = entity.Rounds.OrderByDescending(r => r.Order).FirstOrDefault();
            if (round == null) return;
            _dbContext.Rounds.Remove(round);
            _dbContext.SaveChanges();
        }

        private static void SetWinner(Match entity)
        {
            var max = entity.Competition.MaxPoints;
            var round = entity.Rounds.LastOrDefault();
            if (round != null)
            {
                foreach (var roundPlayerStatse in round.Players)
                {
                    if (roundPlayerStatse.Status != Status.Left)
                        if ((roundPlayerStatse.CurrentScore + roundPlayerStatse.PointsLost) > max)
                            roundPlayerStatse.Status = Status.Snap;
                }
                if (round.Players.All(p => p.Status == Status.Snap || p.Status == Status.Win || p.Status == Status.Left))
                {
                    var winner = round.Players.FirstOrDefault(p => p.Status == Status.Win);
                    if (winner != null) entity.Winner = winner.Player;
                    foreach (var roundPlayerStatse in round.Players)
                    {
                        if (roundPlayerStatse.Status != Status.Win)
                            roundPlayerStatse.Status = Status.Lose;
                    }
                }
            }
        }

        private void BindRound(RoundDto model, Round entity)
        {
            entity.Carding = _dbContext.Players.Find(model.Carding);
            entity.Order = model.RoundNo;


            entity.Players = model.Players.Select(p => BindRoundPlayerStats(p, entity)).ToList();
        }

        private RoundPlayerStats BindRoundPlayerStats(RoundPlayerStatsDto model, Round round)
        {
            return new RoundPlayerStats
            {
                CurrentScore = model.CurrentScore,
                Status = model.Status,
                Id = model.Id,
                Player = _dbContext.Players.Find(model.PlayerId),
                PointsLost = model.PointsLost,
                Round = round
            };
        }

        public static MatchDto ProjectMatchDetailed(Match entity)
        {
            var dto = new MatchDto
            {
                Id = entity.Id,
                Winner = ProjectPlayer(entity.Winner),
                CompetitionId = entity.Competition.Id,
                Competition = entity.Competition.Name,
                Date = entity.Date,
                Rounds = entity.Rounds.Select(r => ProjectRound(entity, r)).ToList(),
                AllPlayers = entity.Rounds.SelectMany(p => p.Players.Select(x => x.Player)).Distinct().Select(ProjectPlayer).ToList(),
                MaxPoints = entity.Competition.MaxPoints
            };
            if (entity.Rounds.Count > 0)
            {
                dto.Players =
                    entity.Rounds.Last()
                        .Players.Where(p => p.Status != Status.Left)
                        .OrderBy(p => p.Order)
                        .Select(p => p.Player)
                        .Select(ProjectPlayer)
                        .ToList();
            }
            return dto;
        }

        public static MatchDto ProjectMatch(Match entity)
        {
            return new MatchDto
            {
                Id = entity.Id,
                Winner = ProjectPlayer(entity.Winner),
                CompetitionId = entity.Competition.Id,
                Competition = entity.Competition.Name,
                Date = entity.Date
            };
        }

        private static RoundDto ProjectRound(Match match, Round round)
        {
            return new RoundDto
            {
                Id = round.Id,
                RoundNo = round.Order,
                CompetitionId = match.Competition.Id,
                Carding = round.Carding?.Id ?? 0,
                MatchId = match.Id,
                Players = round.Players.OrderBy(p => p.Order).Select(ProjectRoundPlayerStats)
            };
        }

        private static PlayerDto ProjectPlayer(Player player)
        {
            if (player == null)
                return null;
            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name
            };
        }

        private static RoundPlayerStatsDto ProjectRoundPlayerStats(RoundPlayerStats entity)
        {
            return new RoundPlayerStatsDto
            {
                Id = entity.Id,
                Status = entity.Status,
                Player = entity.Player.Name,
                PointsLost = entity.PointsLost,
                CurrentScore = entity.CurrentScore,
                PlayerId = entity.Player.Id,
                Order = entity.Order
            };
        }
    }
}