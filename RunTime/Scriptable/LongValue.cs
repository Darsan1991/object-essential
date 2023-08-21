using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/LongValue", fileName = "LongValue", order = 6)]
    public class LongValue : Value<long>
    {
        
    }
}