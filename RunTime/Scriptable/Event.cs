using System;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardMessage("This is used for Game event Purpose. You don't have to use this unless you are changing some game logic. You can create event here.",false)]
    [DashboardType(tabPath:"System/Events")]
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