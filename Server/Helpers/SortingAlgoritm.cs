using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using knatteligan.Domain.Entities;
using knatteligan.Repositories;

namespace knatteligan.Helpers
{
    public class SortingAlgoritm
    {
        public static List<Player> Sort(List<Player> list, PlayerSortByTypes types)
        {
            switch (types)
            {
                case PlayerSortByTypes.Goal:
                    return list
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllGoals()
                                    .Count(matchVent => matchVent.PlayerGuid == p.Id))
                        .ToList();
                case PlayerSortByTypes.Assist:
                    return list
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllAssists()
                                    .Count(matchVent => matchVent.PlayerGuid == p.Id))
                        .ToList();
                case PlayerSortByTypes.Redcard:
                    return list
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllRedCards()
                                    .Count(matchVent => matchVent.PlayerGuid == p.Id))
                        .ToList();
                case PlayerSortByTypes.Yellowcard:
                    return list
                        .Where(p => p.MatchEvents.Count > 0)
                        .OrderBy(
                            p =>
                                MatchEventRepository.GetInstance()
                                    .GetAllYellowCards()
                                    .Count(matchVent => matchVent.PlayerGuid == p.Id))
                        .ToList();
                case PlayerSortByTypes.PlayerName:

                    return list
                        .Where(p => p.MatchEvents.Count >= 0)
                        .OrderBy( p => p.Name.Name).ToList();
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