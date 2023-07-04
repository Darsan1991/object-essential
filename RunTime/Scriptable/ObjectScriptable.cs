using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    public abstract partial class ObjectScriptable : ScriptableObject,IObjectItem
    {
        public virtual void ResetOnBuild()
        {
            
        }
    }
    
#if DGAMES_SERVICES

    public abstract partial class ObjectScriptable 
    {
        [SerializeField] protected bool registerGlobally;
        public bool RegisterGlobally => registerGlobally;


      
    }
    
#endif

    
#if DGAMES_SERVICES

    public interface IRegisterGlobally
    {
        public bool RegisterGlobally { get; }

    }
    #endif
}