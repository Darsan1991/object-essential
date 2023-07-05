using System;
using System.Collections.Generic;
using System.Linq;

namespace DGames.ObjectEssentials
{
    public class BinderBase
    {

        [NonSerialized]protected readonly List<BindSettings> binds = new();
        
        protected void Bind(object raised, object binding)
        {
            if (binds.Any(b=>b.binding == binding))
            {
                throw new InvalidOperationException();
            }

            binds.Add(new BindSettings
            {
                binding = binding,
                raised = raised
            });
        }

        protected void Bind(object raised, string id)
        {
            if (binds.Any(b=>b.id == id))
            {
                throw new InvalidOperationException();
            }
            
            binds.Add(new BindSettings
            {
                id = id,
                raised = raised
            });

           
        }

        public void UnBind(object binding)
        {
            binds.RemoveAll(b => b.binding == binding);
        }

        public void UnBind(string id)
        {
            binds.RemoveAll(b => b.id == id);
        }

        public bool HasBinding(object binding) => binds.Any(b => b.binding == binding);
        public bool HasBinding(string id) => binds.Any(b => b.id == id);

        public void Clear()
        {
            binds.Clear();
        }

        public void ClearEmpty()
        {
            binds.RemoveAll(b => b.binding == null && string.IsNullOrEmpty(b.id));
        }

        public struct BindSettings
        {
            public object binding;
            public object raised;
            public string id;
        }
    }
}