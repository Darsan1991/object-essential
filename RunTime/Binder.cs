using System;
using System.Linq;
using DGames.ObjectEssentials.Extensions;

namespace DGames.ObjectEssentials
{
    public class Binder<T,TJ,TJj> : BinderBase
    {
        public void Raised(T valueOne,TJ valueTwo,TJj valueThree)
        {
            binds.Where(b=>b.raised!=null).Select(b=>(Action<T,TJ,TJj>)b.raised).ForEach(c=>c(valueOne,valueTwo,valueThree));
        }

        public void Bind(Action<T,TJ,TJj> raised, object binding) => Bind(raised as object,binding);
        public void Bind(Action<T,TJ,TJj> raised, string id) => Bind(raised as object,id);
    }

    public class Binder<T, TJ> : BinderBase
    {
        public void Raised(T valueOne, TJ valueTwo)
        {
            binds.Where(b => b.raised != null).Select(b => (Action<T, TJ>)b.raised).ForEach(c => c(valueOne, valueTwo));
        }

        public void Bind(Action<T, TJ> raised, object binding) => Bind(raised as object, binding);
        public void Bind(Action<T, TJ> raised, string id) => Bind(raised as object, id);
    }

    public class Binder<T> : BinderBase
    {
        public void Raised(T value)
        {
            binds.Where(b => b.raised != null).Select(b => (Action<T>)b.raised).ForEach(c => c(value));
        }

        public void Bind(Action<T> raised, object binding) => Bind(raised as object, binding);
        public void Bind(Action<T> raised, string id) => Bind(raised as object, id);
    }
    
    
}
