using System;
using UnityEngine;

namespace DGames.ObjectEssentials.Scriptable
{
    // ReSharper disable once HollowTypeName
    public static class PrefManager
    {
        public static TJ Get<TJ>(string s, TJ def)
        {
            if (IsBuildInType(typeof(TJ)))
                return GetBuildInValue(s, def);

            return PlayerPrefs.HasKey(s) ? JsonUtility.FromJson<TJ>(PlayerPrefs.GetString(s)) : def;
        }

        public static void Set<TJ>(string s, TJ value)
        {
            if (IsBuildInType(typeof(TJ)))
            {
                SetBuildInValue(s, value);
                return;
            }

            PlayerPrefs.SetString(s, JsonUtility.ToJson(value));
        }

        private static void SetBuildInValue<TJ>(string key, TJ value)
        {
            var type = typeof(TJ);
            if (type == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)(object)value);
            }

            if (type == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)(object)value);
            }

            if (type == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)(object)value);
            }


            if (type == typeof(bool))
            {
                PlayerPrefs.SetInt(key, (bool)(object)value ? 1 : 0);
            }
        }


        private static TJ GetBuildInValue<TJ>(string key, TJ def)
        {
            var type = typeof(TJ);
            if (type == typeof(int))
            {
                return (TJ)(object)PlayerPrefs.GetInt(key, (int)(object)def);
            }

            if (type == typeof(string))
            {
                return (TJ)(object)PlayerPrefs.GetString(key, (string)(object)def);
            }

            if (type == typeof(float))
            {
                return (TJ)(object)PlayerPrefs.GetFloat(key, (float)(object)def);
            }


            if (type == typeof(bool))
            {
                return (TJ)(object)(PlayerPrefs.GetInt(key, (bool)(object)def ? 1 : 0) > 0);
            }

            return def;
        }

        private static bool IsBuildInType(Type type)
        {
            return type == typeof(int) || type == typeof(string) || type == typeof(float) || type == typeof(bool);
        }

        public static void Delete(string s)
        {
            PlayerPrefs.DeleteKey(s);
        }
    }
}