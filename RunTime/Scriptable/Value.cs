using System;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [HideScriptField]
    
    public abstract class Value : ObjectScriptable, IValue
    {
        public event Action<IValue> Changed;

        [HideInInspector] [SerializeField] protected string id;
        [HorizontalLayout()][SerializeField] protected bool isTemp;

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
        protected void PrintValue()
        {
            Debug.Log(GetValue());
        }

        [ContextMenu(nameof(ClearValue))]
        protected virtual void ClearValueContext()
        {
            ClearValue();
        }

        protected abstract void ClearValue();
    }

    public class Value<T> : Value, IValue<T>
    {

        [HorizontalLayout()][SerializeField] protected T _initialValue;
        [NonSerialized] private T _value;

        public Binder<T> Binder { get; } = new();

        public T CurrentValue
        {
            get
            {
                if (!isTemp && !Cached)
                {
                    Cached = true;
                    return _value = PrefManager.Get(id, _initialValue);
                }

                return _value;
            }
        }


        [field: NonSerialized] public bool Cached { get; private set; }

        public static implicit operator T(Value<T> value) => value.CurrentValue;

        public override object GetValue() => Get();

        public T Get() => CurrentValue;

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

        public virtual void OnEnable()
        {
            if (isTemp) return;
            Cached = false;
            _value = _initialValue;
        }


        public virtual void OnDisable()
        {
            if (isTemp) return;
            Cached = false;
            _value = _initialValue;
        }

        public override void ResetOnBuild()
        {
            base.ResetOnBuild();
            _value = _initialValue;
            Cached = false;
        }

        protected override void ClearValue()
        {
            if (isTemp)
            {
                Debug.Log("The Value is Temp");
                return;
            }

            _value = _initialValue;
            PrefManager.Delete(id);
            Debug.Log($"{name} Cleared!");
        }

       
    }

   
    // ReSharper disable once HollowTypeName
}