using System;
using System.Collections;
using DGames.ObjectEssentials;
using UnityEngine.UI;

namespace DGames.ObjectEssentials
{
    public static class ValueExtensions
    {
        public static void Bind(this IValue value, Text text,string format = "{0}")
        {
            value.BaseBinder.Bind(_=>text.text = string.Format(format,value),text);
        }
        
        public static void Bind<T>(this IValue<T> value, Text text,string format = "{0}")
        {
            value.BaseBinder.Bind(_=>text.text = string.Format(format,value),text);
        }

        public static void Bind<T>(this IValue<T> value, Text text, Func<Text,T,IEnumerator> anim,UnityEngine.MonoBehaviour runner)
        {
            value.BaseBinder.Bind(_=>
            {
                runner.StartCoroutine(anim(text, value.Get()));
            },text);
        }

        public static void UnBind(this IValue value, object binding) => value.BaseBinder.UnBind(binding);
        public static void UnBind<T>(this IValue<T> value, object binding)
        {
            value.Binder.UnBind(binding);
            value.BaseBinder.UnBind(binding);
        }
    }
}