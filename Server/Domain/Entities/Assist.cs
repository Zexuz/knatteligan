using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class Assist : MatchEvent
    {
        public override Guid PlayerGuid { get; set; }
        public override Guid MatchGuid { get; set; }

        public Assist(Guid playerGuid, Guid matchGuid)
        {
            PlayerGuid = playerGuid;
            MatchGuid = matchGuid;
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
            var player = PersonRepository.GetInstance().FindBy(PlayerGuid);
            return $"Assist :{player.Name}({player.PersonalNumber})";
        }
    }
}