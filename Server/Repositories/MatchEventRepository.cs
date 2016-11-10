using knatteligan.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace knatteligan.Repositories
{
    public class MatchEventRepository : Repository<MatchEvent>
    {
        protected override string FilePath { get; }
        private readonly List<MatchEvent> _matchEvent;


        public MatchEventRepository()
        {
            FilePath = GetFilePath("MatchEvents.xml");
            _matchEvent = Load().ToList();
        }
        public void Add(MatchEvent matchEvent)
        {
            AddAndSaveYellowCard(matchEvent);
        }

        private void AddAndSaveYellowCard(MatchEvent matchEvent)
        {
            _matchEvent.Add(matchEvent);
            Save(_matchEvent);
        }
        public static MatchEventRepository GetInstance()
        {
            return (MatchEventRepository)(Repo ?? (Repo = new MatchEventRepository()));
        }

        public override IEnumerable<MatchEvent> GetAll()
        {
            return _matchEvent;
        }
    }
}
