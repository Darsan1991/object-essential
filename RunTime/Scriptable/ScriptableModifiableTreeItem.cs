using System;
using System.Collections.Generic;
using System.Linq;
using DGames.ObjectEssentials.Extensions;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public abstract partial class ScriptableModifiableTreeItem<TItem,TInterface> : ScriptableObject, IModifiableTreeItem<TInterface>
        where TItem : ScriptableModifiableTreeItem<TItem,TInterface>,TInterface where TInterface : IModifiableTreeItem<TInterface>
    {
        public event Action<TInterface> Modified;

        [SerializeField] protected List<TItem> children = new();
        [HideInInspector] [SerializeField] protected TItem parent;

        public TInterface Parent
        {
            get => parent;
            set
            {
                if (parent != null)
                {
                    parent.RemoveChild((TItem)this);
                    OnRemoveFromParent(parent);
                    parent.MarkDirtyIfEditor();
                }

                parent = (TItem)value;

                if (parent != null)
                {
                    parent.AddChild((TItem)this);
                    OnAddedToParent(parent);
                    parent.MarkDirtyIfEditor();
                }

                MarkDirtyIfEditor();
            }
        }

        public IEnumerable<TInterface> Children => children;


        protected virtual void OnRemoveFromParent(TInterface parentItem)
        {
        }

        protected virtual void OnAddedToParent(TInterface patentItem)
        {
        }


        protected virtual void ChildOnModified(TInterface item)
        {
            NotifyModify();
        }

        protected virtual void OnChildrenModified()
        {
            NotifyModify();
        }


        protected virtual void OnModified()
        {
            NotifyModify();
        }


        protected void NotifyModify()
        {
            Modified?.Invoke((TItem)this);
        }

        private void AddChild(TItem value)
        {
            value.Modified += ChildOnModified;
            children.Add(value);
            OnChildrenModified();
        }

        private void RemoveChild(TItem item)
        {
            item.Modified -= ChildOnModified;
            children.Remove(item);
            OnChildrenModified();
        }


        protected void MarkDirtyIfEditor()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public partial class ScriptableModifiableTreeItem<TItem,TInterface>
    {
        [HideInInspector] [SerializeField] protected List<TItem> lastChildren = new();


        protected virtual void OnValidate()
        {
            HandleParentValueForChildren();
            OnModified();
        }

        protected void HandleParentValueForChildren()
        {
            RemoveNullIfSomethingDeleted();

            var newComers = HandleNewComers();

            var outGoers = HandleOutGoers();

            if (newComers.Any() || outGoers.Any())
                lastChildren = children.ToList();
        }

        private IEnumerable<TItem> HandleOutGoers()
        {
            var outGoers = lastChildren.Except(children).ToArray();
            // ReSharper disable once RedundantCast
            children.AddRange(outGoers);
            // ReSharper disable once RedundantCast
            outGoers.Where(g => Equals(g.Parent, (TInterface)(TItem)this)).ForEach(t => t.Parent = default);
            return outGoers;
        }

        private IEnumerable<TItem> HandleNewComers()
        {
            var newComers = children.Except(lastChildren).ToArray();
            children.RemoveAll(c => newComers.Contains(c));
            newComers.ForEach(c => c.Parent = (TItem)this);
            return newComers;
        }

        private void RemoveNullIfSomethingDeleted()
        {
            if (lastChildren.Any(c => !c))
            {
                children.RemoveAll(c => !c);
                lastChildren.RemoveAll(c => !c);
            }
        }
    }
}