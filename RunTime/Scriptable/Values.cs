using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    [CreateAssetMenu(menuName = "Create Value Group", fileName = "Values", order = 0)]
    public partial class Values : ScriptableObject
    {
        [SerializeField] protected List<Values> children = new();
        [SerializeField] protected List<KeyAndValue> items = new();

        private readonly Dictionary<string, IValue> _keyVsValueCache = new();

        public IValue this[string key]
        {
            get
            {
                CacheIfNotAlready();
                if (!_keyVsValueCache.ContainsKey(key))
                    Debug.LogWarning("Value Key NotFound:" + key);
                return _keyVsValueCache.GetValueOrDefault(key);
            }
        }

        private void CacheIfNotAlready()
        {
            if (_keyVsValueCache.Any())
            {
                return;
            }

            foreach (var pair in this)
            {
                _keyVsValueCache.Add(pair.key, pair.value);
            }
        }

        public IValue<T> Get<T>(string key)
        {
            return this[key] as IValue<T>;
        }
    }

    public partial class Values
    {
        private static Values _default;
        public static Values Default => _default ??= Resources.Load<Values>($"{DEFAULT_FOLDER_PATH}/{nameof(Values)}");
        public const string DEFAULT_FOLDER_PATH = "ValueGroups";
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyGames/Values")]
        public static void Open()
        {
            ScriptableEditorUtils.OpenOrCreateDefault<Values>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }

    public partial class Values : IEnumerable<KeyAndValue>
    {
        public IEnumerator<KeyAndValue> GetEnumerator()
        {
            return items.Concat(children.SelectMany(c => c)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public partial class Values
    {
        public const string CHILDREN_FIELD = nameof(children);
        public const string KEY_VALUE_FIELD = nameof(items);
    }
}