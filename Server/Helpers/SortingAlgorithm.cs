using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Domain.ValueObjects;
using knatteligan.Repositories;

namespace knatteligan.Helpers
{
    public class SortingAlgorithm
    {
        private static List<Guid> GetMatchEventsIds(Player player, MatchEvents type)
        {
            return player.MatchEvents
                .Select(mId => MatchEventRepository.GetInstance().FindById(mId))
                .Where(matchEvent => matchEvent.GetType() == type)
                .Select(mEvent => mEvent.Id).ToList();
        }

        public static IEnumerable<PlayerStatsInfoItem> Sort(IEnumerable<Team> teams, PlayerSortByTypes type, bool desc)
        {
            var list = (
                from team in teams
                from playerId in team.PlayerIds
                select PersonRepository.GetInstance().FindPlayerById(playerId)
                into player
                select new PlayerStatsInfoItem
                {
                    Id = player.Id,
                    Name = player.Name,
                    PersonalNumber = player.PersonalNumber,
                    AssistIds = GetMatchEventsIds(player, MatchEvents.Assist),
                    GoalIds = GetMatchEventsIds(player, MatchEvents.Goal),
                    YellowCardIds = GetMatchEventsIds(player, MatchEvents.YellowCard),
                    RedCardIds = GetMatchEventsIds(player, MatchEvents.RedCard),
                    TeamName = TeamRepository.GetInstance().FindTeamByPlayerId(player.Id).Name
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
                            orderby list.GoalIds.Count descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.GoalIds.Count
                        select list
                    ).ToList();

                case PlayerSortByTypes.Assist:

                    if (desc)
                        return (
                            from list in players
                            orderby list.AssistIds.Count descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.AssistIds.Count
                        select list
                    ).ToList();

                case PlayerSortByTypes.Redcard:
                    if (desc)
                        return (
                            from list in players
                            orderby list.RedCardIds.Count descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.RedCardIds.Count
                        select list
                    ).ToList();
                case PlayerSortByTypes.Yellowcard:
                    if (desc)
                        return (
                            from list in players
                            orderby list.YellowCardIds.Count descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.YellowCardIds.Count
                        select list
                    ).ToList();
                case PlayerSortByTypes.PlayerName:
                    if (desc)
                        return (
                            from list in players
                            orderby list.Name.Name descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.Name.Name
                        select list
                    ).ToList();
                case PlayerSortByTypes.TeamName:
                    if (desc)
                        return (
                            from list in players
                            orderby list.TeamName.Value descending
                            select list
                        ).ToList();

                    return (
                        from list in players
                        orderby list.TeamName.Value
                        select list
                    ).ToList();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public class PlayerStatsInfoItem
        {
            public List<Guid> GoalIds { get; set; }
            public List<Guid> AssistIds { get; set; }
            public List<Guid> RedCardIds { get; set; }
            public List<Guid> YellowCardIds { get; set; }

            public PersonName Name { get; set; }
            public PersonalNumber PersonalNumber { get; set; }
            public Guid Id { get; set; }
            public TeamName TeamName { get; set; }
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