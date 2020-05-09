using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class HeapSortArray<T>
    {
        int OperationCount;
        public int getOpCount()
        {
            return OperationCount;
        }
        public void sort(T[] arr)
        {
            int n = arr.Length;
            OperationCount += 1;
            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                OperationCount += 2;
                heapify(arr, n, i);
            }
            // One by one extract an element from heap 
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end 
                T temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                OperationCount += 4;
                // call max heapify on the reduced heap 
                heapify(arr, i, 0);
            }
        }

        // To heapify a subtree rooted with node i which is 
        // an index in arr[]. n is size of heap 
        void heapify(T[] arr, int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 
            OperationCount += 3;
            // If left child is larger than root 
            if (l < n && 1 == Comparer<T>.Default.Compare(arr[l], arr[largest]))
            {
                largest = l;
                OperationCount += 1;
            }
            OperationCount += 1;
            // If right child is larger than largest so far 
            if (r < n && 1 == Comparer<T>.Default.Compare(arr[r], arr[largest]))
            {
                largest = r;
                OperationCount += 1;
            }
            OperationCount += 1;
            // If largest is not root 
            if (largest != i)
            {
                T swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                OperationCount += 4;
                // Recursively heapify the affected sub-tree 
                heapify(arr, n, largest);
            }
            OperationCount += 1;
        }

        /* A utility function to print array of size n */
        public void printArray(T[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
        }
    }
}
