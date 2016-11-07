using System;

namespace knatteligan.Domain.Entities
{
    public sealed class YellowCard:MatchEvent
    {
        public override Guid PlayerGuid { get; set; }
        public override Guid MatchGuid { get; set; }

        public YellowCard(Guid playerGuid, Guid matchGuid)
        {
            PlayerGuid = playerGuid;
            MatchGuid = matchGuid;
        }
        public YellowCard()
        {

        }

        public override MatchEvents GetType()
        {
            return MatchEvents.YellowCard;
        }

    }
}
