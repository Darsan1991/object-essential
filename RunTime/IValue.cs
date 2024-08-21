using System;
using UnityEngine;

namespace DGames.ObjectEssentials
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommandItem<T> : ICommandItem
    {
        public T Args => Equals(ArgsValue, default(T)) ? default : (T)ArgsValue;
        public void Execute(T args) => Execute(Equals(args, default(T)) ? default : (object)args);
    }

    public interface IQueryItem : IObjectItem, IQuery
    {
        Func<object, object> Runner { get; set; }

        object IQuery.Ask(object obj)
        {
            if (Runner == null)
            {
                Debug.LogWarning("No Runner Found For:" + this);
                return default;
            }

            return Runner(obj);
        }
    }

    public interface IQueryItem<out T> : IQueryItem
    {
        T Ask()
        {
            var result = Ask(null);

            return Equals(result, default(T)) ? default : (T)result;
        }
    }

    public interface IQueryItem<in T, out TJ> : IQueryItem
    {
        TJ Ask(T args)
        {
            var result = Ask(args as object);
            return Equals(result, default(TJ)) ? default : (TJ)result;
        }
    }

    public interface IQuery
    {
        object Ask(object obj);
    }

    public interface ICommandItem : IObjectItem, ICommand
    {
        public object ArgsValue { get; set; }
        public Action<ICommandItem> Action { get; set; }

        void ICommand.Execute()
        {
            Execute(null);
        }

        public void Execute(object args)
        {
            ArgsValue = args;
            Execute();
        }
    }

    public interface IValue<T> : IValue, IBinderProvider<T>, IBaseValue<T>

    {
    }


    public interface IBaseValue<T>
    {
        T Get();
        void Set(T value);
    }
    
    public interface IBaseValue  
    {
        object GetValue();
        void SetValue(object value);
    }
    
    

    public interface IValue : IBaseValue, IObjectItem, IBaseBinderProvider
    {
        event Action<IValue> Changed;
        
    }


    public interface IObjectItem
    {
    }

    public interface IBaseBinderProvider
    {
        Binder<object> BaseBinder { get; }
    }

    public interface IBinderProvider<T>
    {
        Binder<T> Binder { get; }
    }
}