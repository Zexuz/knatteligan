using System.Collections.Generic;

namespace knatteligan.Repositories {

    public interface IRepository<T> {

        void Add();
        void Save(List<T> list);
        IEnumerable<T> GetAll();
        IEnumerable<T> Load();
        IRepository<T> GetInstace();
    }

}