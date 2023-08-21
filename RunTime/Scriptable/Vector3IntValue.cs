using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]

    [CreateAssetMenu(menuName = "Values/Vector3Int", fileName = "Vector3Int", order = 10)]
    public class Vector3IntValue : Value<Vector3Int>
    {
        
    }
}