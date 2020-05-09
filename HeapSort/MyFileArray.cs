using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class MyFileArray
    {
        int OperationCount;
        public int getOpCount()
        {
            return OperationCount;
        }
        public MyFileArray(string fileName, int n)
        {
            double[] data = new double[n];
            int[] dataInt = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                data[i] = rnd.NextDouble();
                dataInt[i] = rnd.Next(1,100);
            }
            try
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    for (int i = 0; i < n; i++)
                    {
                        bw.Write(data[i]);
                        bw.Write(dataInt[i]);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            fs = new FileStream("array.dat", FileMode.Open, FileAccess.ReadWrite);
        }
        FileStream fs { get; set; }
        public IntFloat this[int index]
        {
            get
            {
                Byte[] data = new Byte[12];
                fs.Seek(12 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 12);
                int resultInt = BitConverter.ToInt32(data, 8);
                double resultDouble = BitConverter.ToDouble(data, 0);
                IntFloat result = new IntFloat(resultInt, (float)resultDouble);
                OperationCount += 7;
                return result;
            }
        }
        public void WriteToFile(int indexToOverRide, IntFloat info)
        {
            Byte[] data;
            fs.Seek(indexToOverRide*12,SeekOrigin.Begin);
            data = BitConverter.GetBytes((double)info.Floater);
            fs.Write(data, 0, 8);
            data = BitConverter.GetBytes(info.Integer);
            fs.Seek(indexToOverRide * 12 + 8, SeekOrigin.Begin);
            fs.Write(data, 0, 4);
            OperationCount += 7;
        }
        public void Swap(int j, int i, MyFileArray arr)
        {
            IntFloat a = arr[j];
            IntFloat b = arr[i];
            Byte[] data = new Byte[24];
            fs.Seek(i * 12, SeekOrigin.Begin);
            data = BitConverter.GetBytes((double)a.Floater);
            fs.Write(data, 0, 8);
            fs.Seek((i * 12) + 8, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a.Integer);
            fs.Write(data, 0, 4);
            fs.Seek(j * 12, SeekOrigin.Begin);
            data = BitConverter.GetBytes((double)b.Floater);
            fs.Write(data, 0, 8);
            fs.Seek((j * 12) + 8, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b.Integer);
            fs.Write(data, 0, 4);
            OperationCount += 14;
        }
        public void Close()
        {
            fs.Close();
        }

        public void sort(MyFileArray arr, int length)
        {
            int n = length;
            OperationCount += 1;
            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                heapify(arr, n, i);
                OperationCount += 2;
            }

            // One by one extract an element from heap 
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end 
                IntFloat temp = arr[0];
                Swap(i, 0, arr);//arr[0] = arr[i];
                arr.WriteToFile(i,temp);//arr[i] = temp;

                // call max heapify on the reduced heap 
                heapify(arr, i, 0);
                OperationCount += 5;
            }
        }

        // To heapify a subtree rooted with node i which is 
        // an index in arr[]. n is size of heap 
        void heapify(MyFileArray arr, int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            // If left child is larger than root 
            if (l < n && 1 == Comparer<IntFloat>.Default.Compare(arr[l], arr[largest]))
            {
                largest = l;
                OperationCount += 1;
            }
            // If right child is larger than largest so far 
            if (r < n && 1 == Comparer<IntFloat>.Default.Compare(arr[r], arr[largest]))
            {
                largest = r;
                OperationCount += 1;
            }
            OperationCount += 4;
            // If largest is not root
            OperationCount += 1;
            if (largest != i)
            {
                IntFloat swapp = arr[i];
                Swap(i, largest, arr);//arr[i] = arr[largest];
                arr.WriteToFile(largest, swapp);//arr[largest] = swapp;
                OperationCount += 4;
                // Recursively heapify the affected sub-tree 
                heapify(arr, n, largest);
            }
        }
    }
}
