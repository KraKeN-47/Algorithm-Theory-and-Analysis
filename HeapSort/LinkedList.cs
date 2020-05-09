using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class LinkedList<T>
    {
        private Node<T> head;
        protected int length;
        public LinkedList() { }
        public void addItem(T value1)
        {
            if (length == 0)     // It means no element in the list
                head = new Node<T>(value1,null);
            else
                this.returnLastNode().Next = new Node<T>(value1,null);
            length++;
        }
        public Node<T> returnLastNode()
        {
            Node<T> tmp = head;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            return tmp;
        }
        public void printAll()
        {
            Node<T> tmp = head;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(tmp.Data + "\t");
                tmp = tmp.Next;
            }
        }
        public Node<T> returnNodeByIndex(int index)
        {
            Node<T> tmp = head;
            for (int i = 0; i < index; i++)
            {
                tmp = tmp.Next;
            }
            return tmp;
        }
        public int returnLength()
        {
            return this.length;
        }
        public void assignLength(int otherLength)
        {
            this.length = otherLength;
        }
        public void clear()
        {
            this.head = null;
            length = 0;
        }
    }
}
