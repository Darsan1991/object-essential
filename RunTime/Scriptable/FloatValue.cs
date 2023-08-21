using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/FloatValue", fileName = "FloatValue", order = 1)]
    public class FloatValue : Value<float>
    {
        
    }
}