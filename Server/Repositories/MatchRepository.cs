using System;
using System.Collections.Generic;
using System.IO;

using knatteligan.Domain.Entities;

namespace knatteligan.Repositories {

    public class MatchRepository : Repositori<Match> {

        protected override string FilePath { get; }

        private MatchRepository() {
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            FilePath = new Uri(Path.Combine(path, "Matches.xml")).LocalPath;
        }

        public override void Add() {
            throw new NotImplementedException();
        }

        public override IEnumerable<Match> GetAll() {
            throw new NotImplementedException();
        }

        public override IRepositori<Match> GetInstace() {
            if (Repo == null) {
                Repo = new MatchRepository();
            }
            return Repo;
        }

    }

}