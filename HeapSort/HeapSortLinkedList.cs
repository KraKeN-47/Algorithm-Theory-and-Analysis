using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class HeapSortLinkedList<T>
    {
        int OperationCount;
        private LinkedList<T> list;
        protected int length;
        public HeapSortLinkedList() { }
        public void init(LinkedList<T> list)
        {
            this.list = list;
            this.length = list.returnLength();
        }
        public virtual void startSort()
        {
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                OperationCount += 2;
                heapify(i, length);
            }
            for (int i = length - 1; i > 0; i--)
            {
                OperationCount += 3;
                swap(0, i);           // swap the first element(biggest) to the sorted group in each time
                heapify(0, i);        // check maxHeap's properties with unsorted group
            }
        }
        public virtual void heapify(int root, int length)
        {
            int left = root * 2 + 1;          // left Child  -> n*2+1
            int right = root * 2 + 2;         // Right Child -> n*2+2
            int maxHeap = root;           // assume root is largest

            /* check whether it fulfills the maxHeap's properties */
            maxHeap = left < length && 1 == Comparer<T>.Default.Compare(list.returnNodeByIndex(left).Data ,list.returnNodeByIndex(maxHeap).Data) ? left : maxHeap;
            maxHeap = right < length && 1 == Comparer<T>.Default.Compare(list.returnNodeByIndex(right).Data, list.returnNodeByIndex(maxHeap).Data) ? right : maxHeap;

            OperationCount += 11;

            if (maxHeap != root)
            {            // It means not fulfill
                OperationCount += 2;
                swap(root, maxHeap);       // swap
                heapify(maxHeap, length);  // check again
            }
            OperationCount += 1;
        }
        public virtual void swap(int i, int j)
        {
            T tmp = list.returnNodeByIndex(i).Data;
            list.returnNodeByIndex(i).Data = list.returnNodeByIndex(j).Data;
            list.returnNodeByIndex(j).Data= tmp;
            OperationCount += 3;
        }
        public int getOpCount()
        {
            return OperationCount;
        }
    }
}
