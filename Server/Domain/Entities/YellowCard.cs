﻿using System;
using knatteligan.Repositories;

namespace knatteligan.Domain.Entities
{
    public sealed class YellowCard : MatchEvent
    {
        public override Guid PlayerGuid { get; set; }
        public override Guid MatchGuid { get; set; }

        public YellowCard() { }

        public YellowCard(Guid playerGuid, Guid matchGuid)
        {
            PlayerGuid = playerGuid;
            MatchGuid = matchGuid;
        }

        public override MatchEvents GetType()
        {
            return MatchEvents.YellowCard;
        }

        public override string ToString()
        {
            var player = PersonRepository.GetInstance().FindBy(PlayerGuid);
            return $"YellowCard: {player.Name}({player.PersonalNumber})";
        }
    }
}
