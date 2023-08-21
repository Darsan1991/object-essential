using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]

    [CreateAssetMenu(menuName = "Values/Vector2", fileName = "Vector2", order = 7)]
    public class Vector2Value : Value<Vector2>
    {
        
    }
}