using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class YellowCard : MatchEvent
    {
        public override Guid PlayerId { get; set; }
        public override Guid MatchId { get; set; }

        public YellowCard() { }

        public YellowCard(Guid playerGuid, Guid matchGuid)
        {
            PlayerId = playerGuid;
            MatchId = matchGuid;
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.YellowCard;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindById(PlayerId);
            return $"YellowCard: {player.Name}({player.PersonalNumber})";
        }
    }
}
