using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [DashboardType(tabPath:"System/Values")]
    [CreateAssetMenu(menuName = "Values/Vector3", fileName = "Vector3", order = 8)]
    public class Vector3Value : Value<Vector3>
    {
        
    }
}