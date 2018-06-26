using System;
using System.Collections.Generic;
namespace AMTDll
{
    public interface IRepository<T>
    {
        bool Create(T newObject);
        bool Update(T newObject);
        List<T> Read();
        bool Remove(T obj);
    }
}
