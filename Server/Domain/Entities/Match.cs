using System;
using System.Collections.Generic;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public class Match : Entity
    {
        public Guid HomeTeam { get; set; }
        public Guid AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }

        public List<Guid> MatchEvents { get; set; }
        public List<Guid> HomeTeamSquad { get; set; }
        public List<Guid> AwayTeamSquad { get; set; }

        public Match() { }

        public Match(Guid homeTeam, Guid awayTeam)
        {

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;

            HomeTeamSquad = new List<Guid>();
            AwayTeamSquad = new List<Guid>();
        }

        public void Swap()
        {
            var tempTeam = HomeTeam;
            HomeTeam = AwayTeam;
            AwayTeam = tempTeam;
        }

        public override string ToString()
        {
            return $"{TeamRepository.GetInstance().FindTeamById(HomeTeam).Name} - {TeamRepository.GetInstance().FindTeamById(HomeTeam).Name}";
        }


    }
}