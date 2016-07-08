using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABus
{
    public class Pipeline<T>
    {

        void Exec(Action action)
        {
            
        }
    }

    internal class NodeList<T>
    {
        public Node<T> Head { get; set; }

        public void AddFirst(T data)
        {
            var toAdd = new Node<T>
            {
                Data = data,
                Next = this.Head
            };

            this.Head = toAdd;
        }

        public void AddLast(T data)
        {
            if (this.Head == null)
            {
                this.Head = new Node<T>();

                this.Head.Data = data;
                this.Head.Next = default(Node<T>);
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
            }
        }

    }

    internal class Node<T>
    {
        public T Data { get; set; }

        public Node<T> Next { get; set; }
    }
}
