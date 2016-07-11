using System.Collections;
using System.Collections.Generic;

namespace ABus.DataStructures
{
    public class GroupedNodeList<T>: IEnumerable<T>
    {
        NodeGroupCollection<T> Groups { get; } = new NodeGroupCollection<T>();

        public void AddLast(string group, T data)
        {
            var nodeGroup = this.GetNodeGroup(group);

            nodeGroup.Nodes.AddLast(data);
        }

        public void AddFirst(string group, T data)
        {
            var nodeGroup = this.GetNodeGroup(group);

            nodeGroup.Nodes.AddFirst(data);
        }

        public void AddBefore(string group, Node<T> node, T data)
        {
            var nodeGroup = this.GetNodeGroup(group);

            nodeGroup.Nodes.AddBefore(node, data);
        }

        public void AddAfter(string group, Node<T> node, T data)
        {
            var nodeGroup = this.GetNodeGroup(group);

            nodeGroup.Nodes.AddAfter(node, data);
        }
        private NodeGroup<T> GetNodeGroup(string group)
        {
            NodeGroup<T> nodeGroup;
            if (this.Groups.Contains(group))
                nodeGroup = this.Groups[group];
            else
            {
                nodeGroup = new NodeGroup<T>(group);
                this.Groups.Add(nodeGroup);
            }
            return nodeGroup;
        }

        /// <summary>
        /// Will linked the tail node of each group to the Head node of the next group that
        /// has at least one node.
        /// </summary>
        /// <remarks>
        /// This method is useful to generate a single linked list from all the available groups
        /// </remarks>
        public Node<T> JoinGroups()
        {
            Node<T> headNode = null;
            Node<T> currentTailNode = null;
            for (int i = 0; i < this.Groups.Count; i++)
            {
                var nodes = this.Groups[i].Nodes;
                if (currentTailNode != null && nodes.Head != null)
                    currentTailNode.Next = nodes.Head;

                if (nodes.Tail != null)
                {
                    currentTailNode = nodes.Tail;
                    if (headNode == null)
                        headNode = nodes.Head;
                }
            }

            return headNode;
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var g in this.Groups)
            {
                if (g.Nodes.Head != null)
                {
                    var node = g.Nodes.Head;
                    yield return node.Data;

                    while (node.Next != null)
                    {
                        node = node.Next;
                        yield return node.Data;
                    }
                }
            }
        }

        public IEnumerable<T> Reverse()
        {
            foreach (var g in this.Groups)
            {
                if (g.Nodes.Tail != null)
                {
                    var node = g.Nodes.Tail;
                    yield return node.Data;

                    while (node.Next != null)
                    {
                        node = node.Previous;
                        yield return node.Data;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}