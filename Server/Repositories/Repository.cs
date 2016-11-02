using System;
using System.Collections.Generic;
using System.IO;

using knatteligan.Helpers;

namespace knatteligan.Repositories {

    public abstract class Repository<T> : IRepository<T> {

        protected static Repository<T> Repo;
        protected abstract string FilePath { get; }

        public abstract IEnumerable<T> GetAll();

        public string GetFilePath(string fileName) {
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            return new Uri(Path.Combine(path, "Matches.xml")).LocalPath;
        }

        public void Save(List<T> list) {
            Serialiser<T>.SaveDataToFile(list, FilePath);
            Load();
        }

        public IEnumerable<T> Load() {
            return Serialiser<T>.GetDataFromFile(FilePath);
        }

    }

}