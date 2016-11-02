using System;
using System.Collections.Generic;

using knatteligan.Domain.Entities;

namespace knatteligan.Repositories {

    public class MatchRepository : Repository<Match> {

        protected override string FilePath { get; }

        private MatchRepository() {
            FilePath = GetFilePath("Matches.xml");
        }


        public override IEnumerable<Match> GetAll() {
            throw new NotImplementedException();
        }

        public static MatchRepository GetInstace() {
            return (MatchRepository) (Repo ?? (Repo = new MatchRepository()));
        }

    }

}