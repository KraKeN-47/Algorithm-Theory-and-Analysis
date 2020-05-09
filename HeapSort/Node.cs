using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node(T Data, Node<T> Next)
        {
            this.Data = Data;
        }
        public Node() { }

        public override string ToString()
        {
            return String.Format($"{Data.ToString()}");
        }
    }
}
