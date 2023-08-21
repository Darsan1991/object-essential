using DGames.Essentials.Attributes;
using DGames.Essentials.EditorHelpers;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    [HideScriptField]
    [ObjectMessage(nameof(comment))]
    public abstract partial class ObjectScriptable : ScriptableObject,IObjectItem
    {
        [ProtectedProperty(editPath:nameof(commentEditing))][TextArea][SerializeField] protected string comment;
        [HideField][SerializeField]protected bool commentEditing;

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
    
    
    #if UNITY_EDITOR
    public partial class ObjectScriptable:ICreatingScriptable
    {
        public bool Creating
        {
            get => commentEditing;
            set => commentEditing = value;
        }
    }
    #endif
    
    
}