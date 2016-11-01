using System.Collections.Generic;

namespace knatteligan.Repositories {

    public interface IRepositori<T> {

        void Add();
        void Save(List<T> list);
        IEnumerable<T> GetAll();
        IEnumerable<T> Load();
        IRepositori<T> GetInstace();

    }

}