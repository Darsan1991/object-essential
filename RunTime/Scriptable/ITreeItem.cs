using System.Collections.Generic;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public interface ITreeItem<T> where T : ITreeItem<T>
    {
        T Parent { get; set; }
        IEnumerable<T> Children { get; }
    }

    public interface ITreeItem
    {
        ITreeItem Parent { get; }
        IEnumerable<ITreeItem> Children { get; }
    }
}