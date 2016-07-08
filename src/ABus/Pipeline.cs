using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABus.Contracts;

namespace ABus
{
    public class ConfigurationPipeline : Pipeline<IPipelineTask>
    {
        protected override async Task InvokeAsync(Node<IPipelineTask> task)
        {
            await task.Data.InvokeAsync(null, async () =>
            {
                if (task.Next != null)
                    await this.InvokeAsync(task.Next);
            });
        }
    }

    public class MessagePipeline : Pipeline<IMessageTask>
    {
        protected override async Task InvokeAsync(Node<IMessageTask> task)
        {
            await task.Data.InvokeAsync(null, null, async () =>
            {
                if (task.Next != null)
                    await this.InvokeAsync(task.Next);
            });
        }
    }

    public abstract class Pipeline<T>
    {
        internal GroupedNodeList<T> TaskGroups { get; set; }

        public void AddLast(string group, T data)
        {
            this.TaskGroups.AddLast(group, data);
        }

        public void AddFirst(string group, T data)
        {
            this.TaskGroups.AddFirst(group, data);
        }

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync()
        {
            foreach (var group in this.TaskGroups.)
            {
                
            }
        }
        protected abstract Task InvokeAsync(Node<T> task);
    }

    public class GroupedNodeList<T>: IEnumerable<T>
    {
        Dictionary<string, NodeGroup<T>> Groups { get; set; } = new Dictionary<string, NodeGroup<T>>();

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

        private NodeGroup<T> GetNodeGroup(string group)
        {
            NodeGroup<T> nodeGroup;
            if (this.Groups.ContainsKey(group))
                nodeGroup = this.Groups[group];
            else
            {
                nodeGroup = new NodeGroup<T>(group);
                this.Groups.Add(group, nodeGroup);
            }
            return nodeGroup;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var g in this.Groups.Values)
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class NodeGroup<T>
    {
        public NodeGroup(string name)
        {
            this.Name = name;
            this.Nodes = new NodeList<T>();
        }

        public NodeList<T> Nodes { get; set; }

        public string Name { get; }
    }

    public class NodeList<T>
    {
        public Node<T> Head { get; set; }

        public Node<T> Tail { get; set; }

        public void AddFirst(T data)
        {
            var toAdd = new Node<T>
            {
                Data = data,
                Next = this.Head
            };

            this.Head = toAdd;
            if (this.Tail == null)
                this.Tail = this.Head;
        }

        public void AddLast(T data)
        {
            if (this.Head == null)
            {
                this.Head = new Node<T>();

                this.Head.Data = data;
                this.Head.Next = default(Node<T>);
                this.Tail = this.Head;
            }
            else
            {
                var toAdd = new Node<T>();
                toAdd.Data = data;

                var current = this.Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toAdd;
                this.Tail = toAdd;
            }
        }

    }

    public class Node<T>
    {
        public T Data { get; set; }

        public Node<T> Next { get; set; }
    }
}
