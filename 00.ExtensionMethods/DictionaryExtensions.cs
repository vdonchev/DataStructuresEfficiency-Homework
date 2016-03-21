namespace _00.ExtensionMethods
{
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static void AddValueToKey<TKey, TCollection, TValue>(
            this IDictionary<TKey, TCollection> dict,
            TKey key,
            TValue value)
                where TCollection : ICollection<TValue>, new()
        {
            TCollection collection;
            if (!dict.TryGetValue(key, out collection))
            {
                collection = new TCollection();
                dict.Add(key, collection);
            }

            dict[key].Add(value);
        }

        public static void EnsureKeyExists<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key)
                where TValue : new()
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new TValue());
            }
        }

        public static void AddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }

            dict[key] = value;
        }

        public static IEnumerable<TValue> GetValuesForKey<TKey, TValue>(
            this IDictionary<TKey, SortedSet<TValue>> dict, TKey key)
        {
            if (!dict.ContainsKey(key))
            {
                return Enumerable.Empty<TValue>();
            }

            return dict[key];
        }

        public static IEnumerable<TValue> GetValuesForKey<TKey, TValue>(
            this IDictionary<TKey, LinkedList<TValue>> dict, TKey key)
        {
            if (!dict.ContainsKey(key))
            {
                return Enumerable.Empty<TValue>();
            }

            return dict[key];
        }
    }
}
