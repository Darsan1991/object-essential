using System.Collections.Generic;

namespace DGames.ObjectEssentials.Scriptable.Utils
{
    public interface ITreeCollection<T> : IModifiableTreeItem<ITreeCollection<T>>, IEnumerable<T>
    {
        
    }
}