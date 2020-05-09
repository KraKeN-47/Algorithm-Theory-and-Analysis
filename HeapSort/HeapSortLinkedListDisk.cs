using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class HeapSortLinkedListDisk<T>
    {
        private LinkedList<T> list;
        protected int length;
        public HeapSortLinkedListDisk() { }
        public void init(LinkedList<T> list)
        {
            this.list = list;
            this.length = list.returnLength();
        }
        public virtual void startSort()
        {
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                heapify(i, length);
            }
            for (int i = length - 1; i > 0; i--)
            {
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

            if (maxHeap != root)
            {            // It means not fulfill
                swap(root, maxHeap);       // swap
                heapify(maxHeap, length);  // check again
            }
        }
        public virtual void swap(int i, int j)
        {
            T tmp = list.returnNodeByIndex(i).Data;
            list.returnNodeByIndex(i).Data = list.returnNodeByIndex(j).Data;
            list.returnNodeByIndex(j).Data = tmp;
        }
    }
}
