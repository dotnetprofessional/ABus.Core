using System.Collections;
using System.Collections.Generic;

namespace ABus.DataStructures
{
    public class NodeList<T> : IEnumerable<T>
    {
        public Node<T> Head { get; set; }

        public Node<T> Tail { get; set; }

        public Node<T> AddFirst(T data)
        {
            var toAdd = new Node<T>
            {
                Data = data,
                Next = this.Head,
            };

            if (this.Head != null)
                this.Head.Previous = toAdd;

            this.Head = toAdd;
            if (this.Tail == null)
                this.Tail = this.Head;

            return toAdd;
        }

        public Node<T> AddLast(T data)
        {
            var toAdd = new Node<T>();
            toAdd.Data = data;

            if (this.Head == null)
            {
                this.Head = toAdd;
                this.Head.Next = default(Node<T>);
                this.Tail = this.Head;
            }
            else
            {
                toAdd.Previous = this.Tail;
                this.Tail.Next = toAdd;

                this.Tail = toAdd;
            }

            return toAdd;
        }

        /// <summary>
        /// Adds node before the supplied node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        public Node<T> AddBefore(Node<T> node, T data)
        {
            var toAdd = new Node<T>
            {
                Data = data,
                Next = node,
                Previous = node.Previous
            };

            node.Previous.Next = toAdd;

            return toAdd;
        }

        /// <summary>
        /// Adds node after the supplied node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        public Node<T> AddAfter(Node<T> node, T data)
        {
            var toAdd = new Node<T>
            {
                Data = data,
                Next = node.Next,
                Previous = node
            };

            node.Next = toAdd;

            return toAdd;
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (this.Head != null)
            {
                var node = this.Head;
                yield return node.Data;

                while (node.Next != null)
                {
                    node = node.Next;
                    yield return node.Data;
                }
            }
        }

        public IEnumerable<T> Reverse()
        {
            if (this.Tail != null)
            {
                var node = this.Tail;
                yield return node.Data;

                while (node.Previous != null)
                {
                    node = node.Previous;
                    yield return node.Data;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}