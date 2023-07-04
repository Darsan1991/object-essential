using System;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public interface IModifiableTreeItem<T> : ITreeItem<T> where T : IModifiableTreeItem<T>
    {
        event Action<T> Modified;
    }
}