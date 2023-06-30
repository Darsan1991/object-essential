using System;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [CreateAssetMenu(menuName = "Create Command", fileName = "Command", order = 0)]
    public class Command : ObjectScriptable,ICommandItem
    {
        public object ArgsValue { get; set; }
        public Action<ICommandItem> Action { get; set; }
    }
}