using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/IntValue", fileName = "IntValue", order = 0)]
    public class IntValue : Value<int>
    {
        

    }
}