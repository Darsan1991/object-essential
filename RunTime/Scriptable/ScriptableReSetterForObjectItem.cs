#if UNITY_EDITOR

using DGames.ObjectEssentials.Extensions;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    class ScriptableReSetterForObjectItem : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            Resources.LoadAll<ObjectScriptable>("").ForEach(s=>s.ResetOnBuild());
        }
    }
}
#endif