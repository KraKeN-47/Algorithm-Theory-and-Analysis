using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeapSort
{
    class Func
    {
        public IntFloat[] GenerateArray(int size)
        {
                Random rand = new Random();
                IntFloat[] Array = new IntFloat[size];
                for (int i = 0; i < size; i++)
                {
                    Array[i] = new IntFloat(rand.Next(0, 1000), (float)rand.NextDouble());
                }
                return Array;
        }
        public void HeapSortRAM(int NumberOfElements)
        {
            int startInt, finishInt;
            int startas = 0; int finisas = 0;
            LinkedList<IntFloat> IntList = new LinkedList<IntFloat>();
            Random random = new Random();
            Stopwatch measure = new Stopwatch();
            HeapSortLinkedList<IntFloat> IntSort = new HeapSortLinkedList<IntFloat>();
            long IntTime;
            int ArrayOP;
            int ListOP;
            for (int y = 0; y < NumberOfElements; y++)
            {
                IntFloat x = new IntFloat(random.Next(0, 1000), (float)random.NextDouble());
                IntList.addItem(x);
            }
            IntFloat[] IntFloatArray = GenerateArray(NumberOfElements);

            Console.WriteLine("Ar norite pamatyti RAM atmintyje sugeneruotas aibes? Įvesti: Y/N");
            string YN = Console.ReadLine();
            bool yesNo = false;
            if (YN == "Y" || YN == "y")
            {
                yesNo = true;
            }
            if (yesNo)
            {
                Console.WriteLine("Nurodykite intervala, kuriuos elementus norite pamatyti:");
                Console.WriteLine("Nuo: ");
                startInt = int.Parse(Console.ReadLine());
                Console.WriteLine("Iki: ");
                finishInt = int.Parse(Console.ReadLine());
                Console.WriteLine("LIST AIBE:");
                for (int i = startInt-1; i < finishInt-1; i++)
                {
                    Console.WriteLine($"{(i+1)}. "+
                    IntList.returnNodeByIndex(i).ToString());
                }
                Console.WriteLine();
                Console.WriteLine("ARRAY AIBE:");
                for (int i = startInt-1; i < finishInt-1; i++)
                {
                    Console.WriteLine($"{(i+1)}. "+IntFloatArray[i].ToString());
                }
            }
            Console.WriteLine("Ar norite pamatyti surikiuotas aibes? Y/N");
            string rikiuotAib = Console.ReadLine();
            bool rikiuotYN = false;
            if (rikiuotAib=="y"||rikiuotAib=="Y")
            {
                rikiuotYN = true;
            }
            Console.WriteLine("\nSorting in OPERATING MEMORY...");
            IntSort.init(IntList);
            measure.Reset();
            measure.Start();
            IntSort.startSort();
            measure.Stop();
            IntTime = measure.ElapsedMilliseconds;
            ListOP = IntSort.getOpCount();
            Console.WriteLine();
            if (rikiuotYN)
            {
                Console.WriteLine($"Nurodykite intervala nuo {1} iki {NumberOfElements}: ");
                Console.WriteLine("Nuo: ");
                startas = int.Parse(Console.ReadLine());
                Console.WriteLine("Iki: ");
                finisas = int.Parse(Console.ReadLine());
                Console.WriteLine("LIST surikiuota aibe:");
                for (int i = startas-1; i <finisas-1 ; i++)
                {
                    Console.WriteLine(IntList.returnNodeByIndex(i).ToString());
                }
            }
            Console.WriteLine($"List sort: {(double)IntTime/1000} ms | Operations made: {ListOP}");
            IntList.clear();

            Console.WriteLine("-------------------------------------");

            long IntArrayTime;
            HeapSortArray<IntFloat> IntArraySort = new HeapSortArray<IntFloat>();
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();
            IntArraySort.sort(IntFloatArray);
            timer.Stop();
            IntArrayTime = timer.ElapsedMilliseconds;
            ArrayOP = IntArraySort.getOpCount();
            if (rikiuotYN)
            {
                Console.WriteLine("Array surikiuota aibe:");
                for (int i = startas - 1; i < finisas - 1; i++)
                {
                    Console.WriteLine(IntFloatArray[i].ToString());
                }
            }
            Console.WriteLine($"Array sort time: {(double)IntArrayTime/1000} ms | Operations made: {ArrayOP.ToString()}");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Items: {NumberOfElements} ===> List: {(double)IntTime/1000} ms | Operations made: {ListOP}  vs  Array: {(double)IntArrayTime/1000} ms | Operations made: {ArrayOP}");
            Console.WriteLine("-----------------------------------------");
        }
        public void HeapSortDISK(int NumberOfElements)
        {
            int startInt = 0; int finishInt = 0;
            Console.WriteLine("\nSorting in DISK memory");
            long time;
            long time2;
            int ArrayOP;
            int ListOP;
            Stopwatch x = new Stopwatch();
            MyFileArray b = new MyFileArray("array.dat", NumberOfElements*12);
            MyFileList a = new MyFileList("test.dat", NumberOfElements*12);

            Console.WriteLine("Ar norite pamatyti DISKINĖJE atmintyje sugeneruotas aibes? Įvesti: Y/N");
            string YN = Console.ReadLine();
            bool yes = false;
            if (YN == "y" || YN == "Y")
            {
                yes = true;
            }

            if (yes)
            {
                Console.WriteLine("Nurodykite intervala, kuriuos elementus norite pamatyti:");
                Console.WriteLine("Nuo: ");
                startInt = int.Parse(Console.ReadLine());
                Console.WriteLine("Iki: ");
                finishInt = int.Parse(Console.ReadLine());
                Console.WriteLine("LIST AIBE:");
                for (int i = startInt - 1; i < finishInt - 1; i++)
                {
                    Console.WriteLine($"{(i + 1)}. " +
                    a.GetByIndex(i).ToString());
                }
                Console.WriteLine();
                Console.WriteLine("ARRAY AIBE:");
                for (int i = startInt - 1; i < finishInt - 1; i++)
                {
                    Console.WriteLine($"{(i + 1)}. " + b[i].ToString());
                }
            }
            Console.WriteLine("Ar norite pamatyti surikiuotas aibes? Y/N");
            string rikiuotiYN = Console.ReadLine();
            bool rikiuotiAibe = false;
            if (rikiuotiYN == "Y" || rikiuotiYN == "y")
            {
                rikiuotiAibe = true;
            }
            
            x.Reset();
                x.Start();
                b.sort(b,NumberOfElements);
                x.Stop();
                time2 = x.ElapsedMilliseconds;
                ArrayOP = b.getOpCount();
            Console.WriteLine($"Finished ARRAY sorting items, elapsed time: {(double)time2/1000}ms  | Operations made: {ArrayOP}");

            Console.WriteLine("---------------------");

                    x.Reset();
                    x.Start();
                    a.startSort(NumberOfElements);
                    x.Stop();
                    time = x.ElapsedMilliseconds;
                    ListOP = a.getOpCount();
                    Console.WriteLine($"Finished LIST sorting items, elapsed time: {(double)time/1000}ms | Operations made: {ListOP}");
            Console.WriteLine("--------------------");
            if (rikiuotiAibe)
            {
                Console.WriteLine("Nurodykite intervala, kuriuos elementus norite pamatyti:");
                Console.WriteLine("Nuo:");
                int pradzia = int.Parse(Console.ReadLine());
                Console.WriteLine("Iki:");
                int pabaiga = int.Parse(Console.ReadLine());
                Console.WriteLine("Diske surikiuotas masyvas:");
                for (int i = pradzia-1; i < pabaiga-1; i++)
                {
                    Console.WriteLine(b[i].ToString());
                }
                Console.WriteLine("Diske surikiuotas LIST:");
                for (int i = pradzia-1; i < pabaiga-1; i++)
                {
                    Console.WriteLine(a.GetByIndex(i).ToString());
                }
            }
            b.Close();
            a.Close();
            Console.WriteLine($"Items: {NumberOfElements} ===> List: {(double)time/1000} ms | Operations: {ListOP}  vs  Array: {(double)time2/1000} ms | Operations: {ArrayOP}");
            Console.ReadKey();
        }
    }
}
