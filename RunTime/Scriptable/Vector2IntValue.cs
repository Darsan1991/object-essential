using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]

    [CreateAssetMenu(menuName = "Values/Vector2Int", fileName = "Vector2Int", order = 9)]
    public class Vector2IntValue : Value<Vector2Int>
    {
        
    }
}