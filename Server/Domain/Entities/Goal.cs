using System;

namespace knatteligan.Domain.Entities
{
    public sealed class Goal : MatchEvent
    {
        public override Guid PlayerGuid { get; set; }
        public override Guid MatchGuid { get; set; }
        public Guid TeamGuid { get; set; }

        public Goal(Guid playerGuid, Guid teamGuid)
        {
            PlayerGuid = playerGuid;
            TeamGuid = teamGuid;
        }

        public Goal()
        {
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.Goal;
        }
    }
}