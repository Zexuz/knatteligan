using knatteligan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knatteligan.Repositories
{
    public class YellowCardRepository : Repository<YellowCard>
    {
        protected override string FilePath { get; }
        private readonly List<YellowCard> _yellowCards;
        public YellowCardRepository()
        {
            FilePath = GetFilePath("YellowCards.xml");
            _yellowCards = Load().ToList();
        }
        public void Add(Guid playerGuid, Guid matchGuid)
        {
            var yellowCard = new YellowCard(playerGuid, matchGuid);
            AddAndSaveYellowCard(yellowCard);
        }

        private void AddAndSaveYellowCard(YellowCard yellowCard)
        {
            _yellowCards.Add(yellowCard);
            Save(_yellowCards);
        }
        public static YellowCardRepository GetInstance()
        {
            return (YellowCardRepository)(Repo ?? (Repo = new YellowCardRepository()));
        }

        public override IEnumerable<YellowCard> GetAll()
        {
            return _yellowCards;
        }
    }
}
