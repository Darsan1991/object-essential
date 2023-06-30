using System;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [CreateAssetMenu(menuName = "Create Query", fileName = "Query", order = 0)]
    public class Query : ObjectScriptable,IQueryItem
    {
        public Func<object, object> Runner { get; set; }
    }
}