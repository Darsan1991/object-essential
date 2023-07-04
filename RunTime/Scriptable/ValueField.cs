using System;
using DGames.ObjectEssentials.Scriptable.Utils;
using UnityEngine;


namespace DGames.ObjectEssentials.Scriptable
{
    [Serializable]
    public class ValueField<T>:ItemField<Value<T>,IValue<T>>
    {
        
        public static implicit operator T(ValueField<T> field) => field.Item.Get();

        public override IValue<T> Item => type switch
        {
            Type.Key => Values.Default.Get<T>(key),
            Type.Scriptable => item,
            _ => throw new ArgumentOutOfRangeException()
        };


        public ValueField(string key) : base(key)
        {
        }
    }
}