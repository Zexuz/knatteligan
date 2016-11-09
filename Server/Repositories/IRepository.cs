using System.Collections.Generic;

namespace knatteligan.Repositories
{

    public interface IRepository<T>
    {
        void Save<TSubT>(string filePath,List<TSubT> list);
        IEnumerable<TSubT> Load<TSubT>(string filePath);
        IEnumerable<T> GetAll();
    }

}