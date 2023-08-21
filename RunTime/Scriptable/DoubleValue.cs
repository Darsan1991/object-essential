using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/DoubleValue", fileName = "DoubleValue", order = 5)]
    public class DoubleValue : Value<double>
    {
        
    }
}