using System.Collections.Generic;
using System.Linq;
using DGames.ObjectEssentials.Extensions;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public abstract class KeyBasedTreeCollection<TKey, TValue, TKeyAndValue> : TreeCollection<TKeyAndValue>,IKeyBasedCollection<TKey,TValue>
        where TKeyAndValue : IKeyAndValue<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _keyVsValues = new();


        public TValue this[TKey key]
        {
            get
            {
                CacheIfNotAlready();
                if (HasSelfContained(key))
                {
                    return _keyVsValues[key];
                }

                var treeCollections = children.Cast<IKeyBasedCollection<TKey, TValue>>()
                    .Where(c => c.HasKey(key)).ToArray();

                return treeCollections.Any() ? treeCollections.First()[key] : default;
            }
        }

        public bool HasKey(TKey key)
        {
            CacheIfNotAlready();
            return HasSelfContained(key) || children.Cast<IKeyBasedCollection<TKey, TValue>>()
                .Any(c => c.HasKey(key));
        }

        public void Add(TKey key, TValue value)
        {
            items.Add(CreateKeyAndValue(key, value));
            OnModified();
        }

        public void UpdateItem(TKey key, TValue value)
        {
            CacheIfNotAlready();
            if (HasSelfContained(key))
            {
                var index = items.FindIndex(i => Equals(i.Key, key));
                items[index] = CreateKeyAndValue(key, value);
                OnModified();
                return;
            }

            children.Cast<IKeyBasedCollection<TKey, TValue>>().Where(c => c.HasKey(key))
                .ForEach(c => c.UpdateItem(key, value));
        }


        protected abstract TKeyAndValue CreateKeyAndValue(TKey key, TValue value);

        public bool HasSelfContained(TKey key)
        {
            CacheIfNotAlready();
            return _keyVsValues.ContainsKey(key);
        }

        protected override void OnModified()
        {
            RefreshCache();
            base.OnModified();
        }


        protected void CacheIfNotAlready()
        {
            if (!_keyVsValues.Any())
                RefreshCache();
        }

        protected virtual TValue GetValueOfItem(IKeyAndValue<TKey, TValue> item)
        {
            return item.Value;
        }

        protected void RefreshCache()
        {
            _keyVsValues.Clear();
            this.ForEach(item => _keyVsValues.Add(item.Key, GetValueOfItem(item)));
        }
    }
    
    public interface IKeyBasedCollection<in TKey,TValue>
    {
        void Add(TKey key, TValue value);
        void UpdateItem(TKey key, TValue value);
        bool HasKey(TKey key);
        TValue this[TKey key] { get; }
    }

    public static class KeyBaseCollectionExtensions
    {
        public static TValue Get<TKey, TValue>(IKeyBasedCollection<TKey, TValue> collection, TKey key,TValue def=default)
        {
            return collection.HasKey(key) ? collection[key] : def;
        }
    }

    public interface IKeyAndValue<out TKey, out TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}