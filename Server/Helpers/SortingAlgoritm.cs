using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Helpers
{
    public class SortingAlgoritm
    {
        private static List<Guid> GetMatchEventsIds(Player player, MatchEvents type)
        {
            return player.MatchEvents
                .Select(mId => MatchEventRepository.GetInstance().FindById(mId))
                .Where(matchEvent => matchEvent.GetType() == type)
                .Select(mEvent => mEvent.Id).ToList();
        }

        public static List<PlayerStatsInfoItem> Sort(List<Team> teams, PlayerSortByTypes type, bool desc)
        {
            var list = (
                from team in teams
                from playerId in team.PlayerIds
                select PersonRepository.GetInstance().FindPlayerById(playerId)
                into player
                select new PlayerStatsInfoItem
                {
                    Id = player.Id,
                    Name = player.Name.Name,
                    PersonalNumber = player.PersonalNumber,
                    AssitsCount = GetMatchEventsIds(player, MatchEvents.Assist).Count,
                    GoalCount = GetMatchEventsIds(player, MatchEvents.Goal).Count,
                    YellowCardCount = GetMatchEventsIds(player, MatchEvents.YellowCard).Count,
                    RedCardCount = GetMatchEventsIds(player, MatchEvents.RedCard).Count,
                    TeamName = TeamRepository.GetInstance().FindTeamByPlayerId(player.Id).Name.Value
                }).ToList();



            return Sort(list,type,desc);
        }

        public static List<PlayerStatsInfoItem> Sort(List<PlayerStatsInfoItem> players, PlayerSortByTypes type, bool desc)
        {
            switch (type)
            {
                case PlayerSortByTypes.Goal:
                    if (desc)
                        return (
                            from list in players
                            orderby list.GoalCount descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.GoalCount
                        select list
                    ).ToList();

                case PlayerSortByTypes.Assist:

                    if (desc)
                        return (
                            from list in players
                            orderby list.AssitsCount descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.AssitsCount
                        select list
                    ).ToList();

                case PlayerSortByTypes.Redcard:
                    if (desc)
                        return (
                            from list in players
                            orderby list.RedCardCount descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.RedCardCount
                        select list
                    ).ToList();
                case PlayerSortByTypes.Yellowcard:
                    if (desc)
                        return (
                            from list in players
                            orderby list.YellowCardCount descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.YellowCardCount
                        select list
                    ).ToList();
                case PlayerSortByTypes.PlayerName:
                    if (desc)
                        return (
                            from list in players
                            orderby list.Name descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.Name
                        select list
                    ).ToList();
                case PlayerSortByTypes.TeamName:
                    if (desc)
                        return (
                            from list in players
                            orderby list.TeamName descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.TeamName
                        select list
                    ).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public class PlayerStatsInfoItem
        {
            public int GoalCount { get; set; }
            public int AssitsCount { get; set; }
            public int RedCardCount { get; set; }
            public int YellowCardCount { get; set; }

            public string Name { get; set; }
            public PersonalNumber PersonalNumber { get; set; }
            public Guid Id { get; set; }
            public string TeamName { get; set; }
        }

        public enum PlayerSortByTypes
        {
            Goal,
            Assist,
            Redcard,
            Yellowcard,
            PlayerName,
            TeamName
        }
    }
}