using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class RedCard : MatchEvent
    {
        public override Guid PlayerGuid { get; set; }
        public override Guid MatchGuid { get; set; }

        public RedCard() { }

        public RedCard(Guid playerGuid, Guid matchGuid)
        {
            PlayerGuid = playerGuid;
            MatchGuid = matchGuid;
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.RedCard;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindBy(PlayerGuid);
            return $"RedCard: {player.Name}({player.PersonalNumber})";
        }
    }
}
