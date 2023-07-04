using System;

using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    public abstract class Value : ObjectScriptable, IValue
    {
        public event Action<IValue> Changed;

        [HideInInspector] [SerializeField] protected string id;
        [SerializeField] protected bool isTemp;

        public Binder<object> BaseBinder { get; } = new();


        public abstract object GetValue();

        protected Value()
        {
            id = Guid.NewGuid().ToString();
        }


        public abstract void SetValue(object value);

        protected virtual void OnChangedEvent()
        {
            BaseBinder.Raised(GetValue());
            Changed?.Invoke(this);
        }
        
        [ContextMenu(nameof(PrintValue))]
        public void PrintValue()
        {
            Debug.Log(GetValue());
        }
    }

    public class Value<T> : Value, IValue<T>
    {
        private T _value;

        public Binder<T> Binder { get; } = new();
        public T CurrentValue => _value.Equals(default) && !isTemp ? _value = PrefManager.Get(id, _value) : _value;

        public static implicit operator T(Value<T> value) => value.CurrentValue;

        public override object GetValue() => Get();

        public T Get() => this;

        public void Set(T value)
        {
            _value = value;
            if (!isTemp)
                PrefManager.Set(id, value);
            OnChangedEvent();
        }

        public override void SetValue(object value)
        {
            Set((T)value);
        }

        protected override void OnChangedEvent()
        {
            base.OnChangedEvent();
            Binder.Raised(this);
        }


        // ReSharper disable once HollowTypeName
        public static class PrefManager
        {
            public static T Get(string s, T def)
            {
                return PlayerPrefs.HasKey(s) ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(s)) : def;
            }

            public static void Set(string s, T value)
            {
                PlayerPrefs.SetString(s, JsonUtility.ToJson(value));
            }
        }
    }


    [Serializable]
    public struct KeyAndValue
    {
        public string key;
        public Value value;
    }


}