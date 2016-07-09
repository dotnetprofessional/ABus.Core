using System.Collections.ObjectModel;

namespace ABus.DataStructures
{
    internal class NodeGroupCollection<T> : KeyedCollection<string, NodeGroup<T>>
    {
        protected override string GetKeyForItem(NodeGroup<T> item)
        {
            return item.Name;
        }
    }
}