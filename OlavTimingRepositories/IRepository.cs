using System.Collections.Generic;

namespace OlavTiming.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Create(IList<T> itemList);
        IList<T> Get();
        IList<T> Get(string filename);
        string[] GetFiles();
        T Update(T item);
        T Delete(int id);
    }
}
