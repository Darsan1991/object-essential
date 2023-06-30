using System;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [CreateAssetMenu(menuName = "Create Event", fileName = "Event", order = 0)]
    public class Event : ObjectScriptable, IEvent
    {
        public event Action<IEvent> Raised;

        public IEventArgs ArgsValue { get; private set; }
        public object Caller { get; private set; }

        public Binder<IEvent> Binder { get; } = new();
        public Binder<object> BaseBinder { get; } = new();



        public void Raise(object caller = null, IEventArgs args = null)
        {
            Caller = caller;
            ArgsValue = args;
            Binder.Raised(this);
            BaseBinder?.Raised(this);
            Raised?.Invoke(this);
        }
    }
}