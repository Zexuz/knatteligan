using System.Collections.Generic;

namespace knatteligan.Repositories {

    public interface IRepository<T> {

        void Save(List<T> list);
        IEnumerable<T> Load();
        IEnumerable<T> GetAll();

    }

}