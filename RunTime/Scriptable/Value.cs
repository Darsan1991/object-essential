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
        [NonSerialized]private T _value;

        public Binder<T> Binder { get; } = new();

        public T CurrentValue
        {
            get
            {
                if (!isTemp && !Cached)
                {
                    Cached = true;
                    return _value = PrefManager.Get<T>(id, default);
                }

                return _value;
            }
        }


        [field: NonSerialized]public bool Cached { get; private set; }

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
            _value = default;
        }
     

        public virtual void OnDisable()
        {
            if (isTemp) return;
            Cached = false;
            _value = default;
        }

        public override void ResetOnBuild()
        {
            base.ResetOnBuild();
            _value = default;
            Cached = false;
        }

        [ContextMenu(nameof(ClearValue))]
        private void ClearValue()
        {
            if (isTemp)
            {
                Debug.Log("The Value is Temp");
                return;
            }

            _value = default;
            PrefManager.Delete(id);
        }

        // ReSharper disable once HollowTypeName
        public static class PrefManager
        {
            public static TJ Get<TJ>(string s, TJ def)
            {
                if (IsBuildInType(typeof(T)))
                    return GetBuildInValue(s, def);
                
                return PlayerPrefs.HasKey(s) ? JsonUtility.FromJson<TJ>(PlayerPrefs.GetString(s)) : def;
            }

            public static void Set<TJ>(string s, TJ value)
            {
                if(IsBuildInType(typeof(T)))
                {
                    SetBuildInValue(s,value);
                    return;
                }

                PlayerPrefs.SetString(s, JsonUtility.ToJson(value));
            }

            private static void SetBuildInValue<TJ>(string key, TJ value)
            {
                var type = typeof(TJ);
                if (type == typeof(int))
                {
                    PlayerPrefs.SetInt(key, (int)(object)value);
                }

                if (type == typeof(string))
                {
                    PlayerPrefs.SetString(key, (string)(object)value);


                }

                if (type == typeof(float))
                {
                    PlayerPrefs.SetFloat(key, (float)(object)value);


                }
                

                if (type == typeof(bool))
                {
                    PlayerPrefs.SetInt(key,(bool)(object)value?1:0);

                }
            }
            
            
            private static TJ GetBuildInValue<TJ>(string key, TJ def)
            {
                var type = typeof(TJ);
                if (type == typeof(int))
                {
                   return (TJ)(object)PlayerPrefs.GetInt(key, (int)(object)def);
                }

                if (type == typeof(string))
                {
                    return (TJ)(object)PlayerPrefs.GetString(key, (string)(object)def);


                }

                if (type == typeof(float))
                {
                    return (TJ)(object)PlayerPrefs.GetFloat(key, (float)(object)def);


                }
                

                if (type == typeof(bool))
                {
                    return (TJ)(object)(PlayerPrefs.GetInt(key,(bool)(object)def?1:0) >0);

                }

                return def;
            }

            private static bool IsBuildInType(Type type)
            {
                return type == typeof(int) || type == typeof(string) || type == typeof(float) || type == typeof(bool);

            }

            public static void Delete(string s)
            {
                PlayerPrefs.DeleteKey(s);
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