using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class Goal : MatchEvent
    {
        public override Guid PlayerId { get; set; }
        public override Guid MatchId { get; set; }
        public Guid TeamId { get; set; }

        public Goal() { }

        public Goal(Guid playerId, Guid teamId, Guid matchId)
        {
            MatchId = matchId;
            PlayerId = playerId;
            TeamId = teamId;
        }


        public override MatchEvents GetType()
        {
            return MatchEvents.Goal;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindById(PlayerId);
            return $"Goal: {player.Name}({player.PersonalNumber})";
        }
    }
}