using System;

namespace DGames.ObjectEssentials
{
    public static class EventExtensions
    {
        public static BinderBase Bind(this IEvent value, Action<IEvent> action,object binding)
        {
            var binder = value.Binder;
            binder.Bind(action,binding);
            return binder;
        }
        
        public static BinderBase Bind<T>(this IEvent<T> value, Action<IEvent,T> action,object binding) where T : IEventArgs
        {
            var binder = value.Binder;
            binder.Bind(e => action?.Invoke(e,((IEvent<T>)e).Args),binding);
            return binder;
        }

        public static void UnBind(this IEvent value, object binding) => value.Binder.UnBind(binding);

        public static T GetArgs<T>(this IEvent value) => (value.ArgsValue is T args) ? args : default;
    }
    
    
    public static class CommandExtensions
    {
        // public static BinderBase Bind(this IEvent value, Action<IEvent> action,object binding)
        // {
        //     var binder = value.Binder;
        //     binder.Bind(action,binding);
        //     return binder;
        // }
        //
        // public static BinderBase Bind<T>(this IEvent<T> value, Action<IEvent,T> action,object binding) where T : IEventArgs
        // {
        //     var binder = value.Binder;
        //     binder.Bind(e => action?.Invoke(e,((IEvent<T>)e).Args),binding);
        //     return binder;
        // }
        //
        // public static void UnBind(this IEvent value, object binding) => value.Binder.UnBind(binding);

        public static T GetArgs<T>(this ICommandItem value) => (value.ArgsValue is T args) ? args : default;
    }
    
    public static class QueryExtensions
    {
        // public static BinderBase Bind(this IEvent value, Action<IEvent> action,object binding)
        // {
        //     var binder = value.Binder;
        //     binder.Bind(action,binding);
        //     return binder;
        // }
        //
        // public static BinderBase Bind<T>(this IEvent<T> value, Action<IEvent,T> action,object binding) where T : IEventArgs
        // {
        //     var binder = value.Binder;
        //     binder.Bind(e => action?.Invoke(e,((IEvent<T>)e).Args),binding);
        //     return binder;
        // }
        //
        // public static void UnBind(this IEvent value, object binding) => value.Binder.UnBind(binding);

        public static T Ask<T, TJ>(this IQuery query,TJ args)
        {
            var result = query.Ask(args);
            return Equals(result, default(T)) ? default : (T)result;
        }
    }
}