namespace _02.BiDictionary
{
    using System;
    using System.Collections.Generic;
    using _00.ExtensionMethods;

    public class BiDictionary<TK1, TK2, T> : IBiDictionary<TK1, TK2, T>
    {
        private readonly Dictionary<TK1, LinkedList<T>> valuesByFirstKey = 
            new Dictionary<TK1, LinkedList<T>>();

        private readonly Dictionary<TK2, LinkedList<T>> valuesBySecondKey = 
            new Dictionary<TK2, LinkedList<T>>();

        private readonly Dictionary<Tuple<TK1, TK2>, LinkedList<T>> valuesByBothKeys = 
            new Dictionary<Tuple<TK1, TK2>, LinkedList<T>>();

        public void Add(TK1 key1, TK2 key2, T value)
        {
            this.valuesByFirstKey.AddValueToKey(key1, value);
            this.valuesBySecondKey.AddValueToKey(key2, value);
            this.valuesByBothKeys.AddValueToKey(new Tuple<TK1, TK2>(key1, key2), value);
        }

        public IEnumerable<T> Find(TK1 key1, TK2 key2)
        {
            var tuple = new Tuple<TK1, TK2>(key1, key2);
            var result = this.valuesByBothKeys.GetValuesForKey(tuple);

            return result;
        }

        public IEnumerable<T> FindByKey1(TK1 key1)
        {
            var result = this.valuesByFirstKey.GetValuesForKey(key1);

            return result;
        }

        public IEnumerable<T> FindByKey2(TK2 key2)
        {
            var result = this.valuesBySecondKey.GetValuesForKey(key2);

            return result;
        }

        public bool Remove(TK1 key1, TK2 key2)
        {
            var search = new Tuple<TK1, TK2>(key1, key2);
            if (this.valuesByBothKeys.ContainsKey(search))
            {
                var distancesToRemove = this.valuesByBothKeys.GetValuesForKey(search);
                this.valuesByBothKeys.Remove(search);
                foreach (var distance in distancesToRemove)
                {
                    this.valuesByFirstKey[key1].Remove(distance);
                    this.valuesBySecondKey[key2].Remove(distance);
                }

                return true;
            }

            return false;
        }
    }
}
