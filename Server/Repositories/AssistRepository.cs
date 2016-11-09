using knatteligan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace knatteligan.Repositories
{
    public class AssistRepository : Repository<Assist>
    {
        protected override string FilePath { get; }
        private readonly List<Assist> _assists;

        public AssistRepository()
        {
            FilePath = GetFilePath("Assists.xml");
            _assists = Load().ToList();
        }
        public void Add(Guid playerGuid, Guid matchGuid)
        {
            var assist = new Assist(playerGuid, matchGuid);
            AddAndSaveAssist(assist);
        }

        private void AddAndSaveAssist(Assist assist)
        {
            _assists.Add(assist);
            Save(_assists);
        }
        public static AssistRepository GetInstance()
        {
            return (AssistRepository)(Repo ?? (Repo = new AssistRepository()));
        }

        public override IEnumerable<Assist> GetAll()
        {
            return _assists;
        }
    }
}
