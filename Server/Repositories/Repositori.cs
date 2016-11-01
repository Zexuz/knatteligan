using System.Collections.Generic;

using knatteligan.Helpers;

namespace knatteligan.Repositories {

    public abstract class Repositori<T> : IRepositori<T> {

        protected static Repositori<T> Repo;
        protected abstract string FilePath { get; }

        public abstract void Add();
        public abstract IEnumerable<T> GetAll();
        public abstract IRepositori<T> GetInstace();

        public void Save(List<T> list) {
            Serialiser<T>.SaveDataToFile(list, FilePath);
            Load();
        }

        public IEnumerable<T> Load() {
            return Serialiser<T>.GetDataFromFile(FilePath);
        }

    }

}