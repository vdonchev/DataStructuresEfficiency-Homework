namespace _02.BiDictionary
{
    using System.Collections.Generic;

    public interface IBiDictionary<TK1, TK2, T>
    {
        void Add(TK1 key1, TK2 key2, T value);

        IEnumerable<T> Find(TK1 key1, TK2 key2);

        IEnumerable<T> FindByKey1(TK1 key1);

        IEnumerable<T> FindByKey2(TK2 key2);

        bool Remove(TK1 key1, TK2 key2);
    }
}