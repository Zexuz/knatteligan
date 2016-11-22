using System;
using System.Collections.Generic;
using System.Linq;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Helpers
{
    public class SortingAlgoritm
    {
        public static List<Player> Sort(List<Player> players, PlayerSortByTypes types)
        {
            switch (types)
            {
                case PlayerSortByTypes.Goal:
                    return players
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllGoals()
                                    .Count(matchEvent => matchEvent.PlayerId == p.Id))
                        .ToList();
                case PlayerSortByTypes.Assist:
                    return players
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllAssists()
                                    .Count(matchEvent => matchEvent.PlayerId == p.Id))
                        .ToList();
                case PlayerSortByTypes.Redcard:
                    return players
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllRedCards()
                                    .Count(matchEvent => matchEvent.PlayerId == p.Id))
                        .ToList();
                case PlayerSortByTypes.Yellowcard:
                    return players
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllYellowCards()
                                    .Count(matchEvent => matchEvent.PlayerId == p.Id))
                        .ToList();
                case PlayerSortByTypes.PlayerName:

                    return players
                        .Where(p => p.MatchEvents.Count >= 0)
                        .OrderBy(p => p.Name.Name).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }
    }

    public enum PlayerSortByTypes
    {
        Goal,
        Assist,
        Redcard,
        Yellowcard,
        PlayerName
    }
}