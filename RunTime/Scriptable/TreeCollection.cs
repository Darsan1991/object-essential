using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DGames.ObjectEssentials.Extensions;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public partial class TreeCollection<TItem> : ScriptableModifiableTreeItem<TreeCollection<TItem>,ITreeCollection<TItem>>, ITreeCollection<TItem>
    {
 

        [SerializeField] protected List<TItem> items = new();


        public TreeCollection<TItem> Root => Parent == null ? this : ((TreeCollection<TItem>)Parent).Root;
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return items.Concat(children.SelectMany(it => it)).GetEnumerator();
        }

     
    }

    public partial class TreeCollection<TItem>
    {
        public void AddItem(TItem item)
        {
            items.Add(item);
            OnModified();
        }

        public void RemoveItem(TItem item)
        {
            RemoveItem((i) => Equals(i, item));
        }

        public bool HasItem(TItem item) => HasItem(i => Equals(i, item));

        public bool HasItem(Func<TItem, bool> predicate) =>
            items.Any(predicate) || children.Any(c => c.HasItem(predicate));

        public void RemoveItem(Func<TItem, bool> predicate)
        {
            if (items.RemoveAll(item => predicate(item)) <= 0)
            {
                children.ForEach(c => c.RemoveItem(predicate));
                return;
            }

            OnModified();
        }

        public void UpdateItem(Func<TItem, bool> predicate, TItem item)
        {
            var indexes = FindAllIndexes(items, predicate).ToList();
            indexes.ForEach(i => items[i] = item);

            if (!indexes.Any())
            {
                children.ForEach(c => c.UpdateItem(predicate, item));
                return;
            }

            OnModified();
        }

        private static IEnumerable<int> FindAllIndexes<T>(IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    yield return i;
                }
            }
        }
    }

}