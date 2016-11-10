using System;
using knatteligan.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace knatteligan.Repositories
{
    public class MatchEventRepository : Repository<MatchEvent>
    {
        #region props

        private readonly List<Goal> _goals;
        private readonly List<Assist> _assists;
        private readonly List<RedCard> _redCards;
        private readonly List<YellowCard> _yellowCards;

        private static string _goalPath;
        private static string _assistPath;
        private static string _redCardsPath;
        private static string _yellowCardsPath;

        #endregion

        public MatchEventRepository()
        {
            _goalPath = GetFilePath("\\GameEvents\\Goals.xml");
            _assistPath = GetFilePath("\\GameEvents\\Assists.xml");
            _redCardsPath = GetFilePath("\\GameEvents\\RedCards.xml");
            _yellowCardsPath = GetFilePath("\\GameEvents\\YellowCards.xml");

            _goals = Load<Goal>(_goalPath).ToList();
            _assists = Load<Assist>(_assistPath).ToList();
            _redCards = Load<RedCard>(_redCardsPath).ToList();
            _yellowCards = Load<YellowCard>(_yellowCardsPath).ToList();
        }

        #region GET

        public IEnumerable<Goal> GetAllGoals()
        {
            return _goals;
        }

        public IEnumerable<Assist> GetAllAssists()
        {
            return _assists;
        }

        public IEnumerable<RedCard> GetAllRedCards()
        {
            return _redCards;
        }

        public IEnumerable<YellowCard> GetAllYellowCards()
        {
            return _yellowCards;
        }

        public override IEnumerable<MatchEvent> GetAll()
        {
            var list = new List<MatchEvent>();
            list.AddRange(_assists);
            list.AddRange(_goals);
            list.AddRange(_redCards);
            list.AddRange(_yellowCards);
            return list;
        }


        #endregion

        #region AddStuff




        public void Add(MatchEvent matchEvent)
        {
            switch (matchEvent.GetType())
            {
                case MatchEvents.RedCard:
                    AddAndSaveRedCard(matchEvent);
                    break;
                case MatchEvents.YellowCard:
                    AddAndSaveYellowCard(matchEvent);
                    break;
                case MatchEvents.Assist:
                    AddAndSaveAssist(matchEvent);
                    break;
                case MatchEvents.Goal:
                    AddAndGoals(matchEvent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddAndSaveAssist(MatchEvent matchEvent)
        {
            var assist = (Assist) matchEvent;
            _assists.Add(assist);
            Save(_assistPath,_assists);
        }

        private void AddAndGoals(MatchEvent matchEvent)
        {
            var goal = (Goal) matchEvent;
            _goals.Add(goal);
            Save(_goalPath,_goals);
        }

        private void AddAndSaveRedCard(MatchEvent matchEvent)
        {
            var redCard = (RedCard) matchEvent;
            _redCards.Add(redCard);
            Save(_redCardsPath,_redCards);

        }

        private void AddAndSaveYellowCard(MatchEvent matchEvent)
        {
            var yellowCard = (YellowCard) matchEvent;
            _yellowCards.Add(yellowCard);
            Save(_yellowCardsPath,_yellowCards);

        }
        #endregion


        public static MatchEventRepository GetInstance()
        {
            return (MatchEventRepository) (Repo ?? (Repo = new MatchEventRepository()));
        }
    }
}