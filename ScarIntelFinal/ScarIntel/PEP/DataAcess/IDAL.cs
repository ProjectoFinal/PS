using System.Collections.Generic;

namespace DataAcess
{
    internal interface IDal<T>
    {
        IEnumerable<T> GetAll();

        T Get(params string[] parameters);
    }
}