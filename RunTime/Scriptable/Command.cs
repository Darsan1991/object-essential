using System;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Command")]
    [CreateAssetMenu(menuName = "Create Command", fileName = "Command", order = 0)]
    public class Command : ObjectScriptable,ICommandItem
    {
        public object ArgsValue { get; set; }
        public Action<ICommandItem> Action { get; set; }
    }
}