using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/StringValue", fileName = "StringValue", order = 4)]
    public class StringValue : Value<string>
    {
        
    }
}