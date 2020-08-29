using System.Collections.Generic;

namespace OlavTiming.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Create(IList<T> itemList);
        IList<T> Get();
        T Update(T item);
        T Delete(int id);
    }
}
