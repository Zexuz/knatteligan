using knatteligan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knatteligan.Repositories
{
    public class RedCardRepository : Repository<RedCard>
    {
        protected override string FilePath { get; }
        private readonly List<RedCard> _redCards;
        public RedCardRepository()
        {
            FilePath = GetFilePath("RedCards.xml");
            _redCards = Load().ToList();
        }
        public void Add(Guid playerGuid, Guid matchGuid)
        {
            var redCard = new RedCard(playerGuid, matchGuid);
            AddAndSaveRedCard(redCard);
        }

        private void AddAndSaveRedCard(RedCard redCard)
        {
            _redCards.Add(redCard);
            Save(_redCards);
        }
        public static RedCardRepository GetInstance()
        {
            return (RedCardRepository)(Repo ?? (Repo = new RedCardRepository()));
        }

        public override IEnumerable<RedCard> GetAll()
        {
            return _redCards;
        }
    }
}
