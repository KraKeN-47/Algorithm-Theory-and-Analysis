using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HeapSort
{
    class MyFileList:HeapSortLinkedList<IntFloat>
    {
        int OperationCount;
        int prevNode, currentNode, nextNode;
        public int getOpCount()
        {
            return OperationCount;
        }
        public MyFileList(string fileName, int n)
        {
            Random rnd = new Random();

            try
            {
                using (BinaryWriter bw = new BinaryWriter
                    (File.Open(fileName,FileMode.Create)))
                {
                    for (int i = 0; i < n; i++)
                    {
                        
                        bw.Write(rnd.NextDouble());
                        bw.Write(rnd.Next(1,1000));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            fs = new FileStream("test.dat", FileMode.Open, FileAccess.ReadWrite);
        }
        FileStream fs { get; set; }
        public IntFloat Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = 0;
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double y = BitConverter.ToDouble(data, 0);
            fs.Seek(8, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            int x = BitConverter.ToInt32(data, 0);
            nextNode = 12;
            IntFloat result = new IntFloat(x, (float)y);
            OperationCount += 13;
            return result;
        }
        public IntFloat Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double y = BitConverter.ToDouble(data, 0);
            fs.Seek(nextNode + 8, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            int x = BitConverter.ToInt32(data, 0);
            IntFloat result = new IntFloat(x, (float)y);
            nextNode += 12;
            OperationCount += 11;
            return result;
        }
        public IntFloat GetByIndex(int i)
        {
            int s = 0;
            OperationCount += 1;
            for (IntFloat a = Head(); Next() != null; a = Next()) // LinkedList principas, praeiti per visą list'ą
            {
                OperationCount += 1;
                if (s == i)
                {
                    Byte[] data = new Byte[12];
                    fs.Seek(i * 12, SeekOrigin.Begin);
                    fs.Read(data, 0, 12);
                    double y = BitConverter.ToDouble(data, 0);
                    int x = BitConverter.ToInt32(data, 8);
                    IntFloat result = new IntFloat(x, (float)y);
                    OperationCount += 7;
                    return result;
                }
                s++;
                OperationCount +=2;
            }
            OperationCount += 1;
            return null;
        }
        public void Close()
        {
            fs.Close();
        }
        public void startSort(int n)
        {
            length = n;
            OperationCount += 1;
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
        public override void heapify(int root, int length)
        {
            int left = root * 2 + 1;          // left Child  -> n*2+1
            int right = root * 2 + 2;         // Right Child -> n*2+2
            int maxHeap = root;           // assume root is largest

            /* check whether it fulfills the maxHeap's properties */
            maxHeap = left < length && 1 == Comparer<IntFloat>.Default.Compare(GetByIndex(left), GetByIndex(maxHeap)) ? left : maxHeap;//4 operacijos
            maxHeap = right < length && 1 == Comparer<IntFloat>.Default.Compare(GetByIndex(right), GetByIndex(maxHeap)) ? right : maxHeap;//4 operacijos
            OperationCount += 11;
            if (maxHeap != root)
            {            // It means not fulfill
                swap(root, maxHeap);       // swap
                OperationCount +=3;
                heapify(maxHeap, length);  // check again
            }
            OperationCount += 1; // if statement
        }
        public override void swap(int i, int j)
        {
            IntFloat a = GetByIndex(j);
            IntFloat b = GetByIndex(i);
            Byte[] data;
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
        }    }
}
