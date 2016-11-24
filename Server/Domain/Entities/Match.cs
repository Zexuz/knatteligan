using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public class Match : Entity
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }

        public List<Guid> MatchEventIds { get; set; }
        public List<Guid> HomeTeamSquadId { get; set; }
        public List<Guid> AwayTeamSquadId { get; set; }

        public bool IsPlayed => HomeTeamSquadId != null && AwayTeamSquadId != null && HomeTeamSquadId.Count > 0 && AwayTeamSquadId.Count > 0;

        public Match() { }

        public Match(Guid homeTeamId, Guid awayTeamId)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;

            HomeTeamSquadId = new List<Guid>();
            AwayTeamSquadId = new List<Guid>();
        }

        public void Swap()
        {
            var tempTeam = HomeTeamId;
            HomeTeamId = AwayTeamId;
            AwayTeamId = tempTeam;
        }

        public override string ToString()
        {
            if (IsPlayed)
            {
                return $"{TeamRepository.GetInstance().FindBy(HomeTeamId).Name} - {TeamRepository.GetInstance().FindBy(AwayTeamId).Name} " +
                  $"{MatchEventRepository.GetInstance().GetAllGoals().Count(x => x.MatchId == this.Id && x.TeamId == HomeTeamId)}" +
                  $" - " +
                  $"{MatchEventRepository.GetInstance().GetAllGoals().Count(x => x.MatchId == this.Id && x.TeamId == AwayTeamId)}";
            }

            return
                $"{TeamRepository.GetInstance().FindBy(HomeTeamId).Name} - {TeamRepository.GetInstance().FindBy(AwayTeamId).Name} ";
        }
    }
}