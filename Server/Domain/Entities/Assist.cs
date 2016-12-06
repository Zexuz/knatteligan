using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class Assist : MatchEvent
    {
        public override Guid PlayerId { get; set; }
        public override Guid MatchId { get; set; }

        public Assist(Guid playerId, Guid matchId)
        {
            PlayerId = playerId;
            MatchId = matchId;
        }

        public Assist()
        {
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.Assist;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindById(PlayerId);
            return $"Assist :{player.Name}({player.PersonalNumber})";
        }
    }
}