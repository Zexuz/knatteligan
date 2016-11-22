using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class RedCard : MatchEvent
    {
        public override Guid PlayerId { get; set; }
        public override Guid MatchId { get; set; }

        public RedCard() { }

        public RedCard(Guid playerGuid, Guid matchGuid)
        {
            PlayerId = playerGuid;
            MatchId = matchGuid;
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.RedCard;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindById(PlayerId);
            return $"RedCard: {player.Name}({player.PersonalNumber})";
        }
    }
}
