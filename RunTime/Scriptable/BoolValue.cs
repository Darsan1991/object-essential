using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/BoolValue", fileName = "BoolValue", order = 2)]
    public class BoolValue : Value<bool>
    {
        
    }
}