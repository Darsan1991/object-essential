// using System;

using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace DGames.ObjectEssentials.Extensions
{
    public static class LinqExtensions
    {

        public static void AddOrUpdate<T, TJ>(this IDictionary<T, TJ> dict, T key, TJ val)
        {
            dict.AddOrUpdate(key,val,out _);
        }
        
        // ReSharper disable once TooManyArguments
        public static void AddOrUpdate<T, TJ>(this IDictionary<T, TJ> dict, T key, TJ val,out bool newKey)
        {
            if (dict.ContainsKey(key))
            {
                newKey = false;
                dict[key] = val;
            }
            else
            {
                newKey = true;
                dict.Add(key, val);
            }
        }

        public static TJ GetOrDefault<T, TJ>(this IDictionary<T, TJ> dict, T key, TJ def=default) =>
            dict.ContainsKey(key) ? dict[key] : def;

        public static T GetRandom<T>(this IEnumerable<T> enumerable, out int index)
        {
            var list = enumerable.ToList();
            index = Random.Range(0, list.Count);
            return list[index];
        }

        public static T GetRandomWithProbabilities<T>(this IEnumerable<T> enumerable, IEnumerable<float> probabilities) => enumerable.GetRandomWithProbabilities(probabilities, 1).FirstOrDefault();

        // ReSharper disable once TooManyArguments
        // ReSharper disable once FlagArgument
        public static IEnumerable<T> GetRandomWithProbabilities<T>(this IEnumerable<T> enumerable, IEnumerable<float> probabilities,int count,bool allowRepeating = true)
        {
            var list = enumerable.ToList();

            var probabilityList = probabilities.ToList();
            var p = Random.Range(0f, probabilityList.Sum());

            for (var i = 0; i < count; i++)
            {
                var index = probabilityList.AggregateFirst((result, item) => result - item, (result) => result <= 0, p);
                yield return list[index];
                if (!allowRepeating)
                {
                    list.RemoveAt(index);
                    probabilityList.RemoveAt(index);
                }
            }
        }

        // ReSharper disable once TooManyArguments
        public static int AggregateFirst<T, TJ>(this IEnumerable<T> enumerable, Func<TJ,T,TJ> aggregate,
            Func<TJ,bool> until, TJ initial = default)
        {
            var aValue = initial;
            var index = 0;
            foreach (var item in enumerable)
            {
                aValue = aggregate(aValue, item);
                if (until(aValue))
                    return index;
                index++;
            }
            return default;
        }



        public static T GetRandom<T>(this IEnumerable<T> enumerable) =>
            enumerable.GetRandom(out _);

        // ReSharper disable once FlagArgument
        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> enumerable, int count,bool allowRepeating = false)
        {
            var list = enumerable.ToList();

            if (list.Count < count)
            {
                throw new InvalidOperationException();
            }

            for (var i = 0; i < count; i++)
            {
                var index = Random.Range(0, list.Count);
                yield return list[index];
                if(!allowRepeating)
                    list.RemoveAt(index);
            }
        }


        public static T GetRandomOrDefault<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();

            if (list.Count == 0)
                return default;

            return list.GetRandom();
        }

        public static T GetAndRemove<T>(this IList<T> list, T item)
        {
            return list.GetAndRemove(list.IndexOf(item));
        }

        public static T GetAndRemove<T>(this IList<T> list, int index)
        {
            if (index < 0 || index >= list.Count)
                return default;
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action?.Invoke(item);
            }
        }
        
    }
}