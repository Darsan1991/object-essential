using System;

namespace DGames.ObjectEssentials
{
    public interface IEvent:IObjectItem,IBaseBinderProvider,IBinderProvider<IEvent>
    {
        event Action<IEvent> Raised;
        IEventArgs ArgsValue { get; }
        object Caller { get; }
        void Raise(object caller=null,IEventArgs args = null);
    }

    public interface IEvent<T>:IEvent where T:IEventArgs
    {
        T Args => (T)ArgsValue;
        void Raise(object caller=null,T args=default);
    }
    
}