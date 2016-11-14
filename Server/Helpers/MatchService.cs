using System;
using System.Collections.Generic;
using knatteligan.Domain.Entities;
using knatteligan.Services;

namespace knatteligan.Helpers
{
    public class MatchService
    {
        private readonly League _league;
        private readonly int _currentMatchWeek;
        private readonly Match _match;

        private readonly MatchRepositoryService _matchRepositoryService;


        public MatchService(Guid matchId)
        {

            _matchRepositoryService = new MatchRepositoryService();

            _match = _matchRepositoryService.Find(matchId);
            _league = MatchHelper.GetLeageFromMatchId(matchId);
            _currentMatchWeek = MatchHelper.GetCurrentMatchWeekNr(_league, matchId);
        }

        public bool IsPlayerSuspended(Guid playerId)
        {
            return _league.MatchWeeks[_currentMatchWeek].SuspendedPlayers.Contains(playerId);
        }

        public void SuspendPlayer(Player player, int rounds, Guid matchId)
        {

            if (_league.MatchWeeks.Count - _currentMatchWeek + rounds < 0)
            {
                rounds = _league.MatchWeeks.Count - _currentMatchWeek;
            }

            for (var i = _currentMatchWeek + 1; i < rounds + _currentMatchWeek; i++)
            {
                _league.MatchWeeks[i].SuspendedPlayers.Add(player.Id);
            }
        }

        public void SetSuspensionLength(List<YellowCard> yellowCards, List<RedCard> redCards, Player player)
        {
            if (yellowCards.Count == 2)
            {
                SuspendPlayer(player, 1, _match.Id);
                return;
            }

            if (redCards.Count == 1)
            {
                SuspendPlayer(player, 3, _match.Id);
            }
        }

    }

}