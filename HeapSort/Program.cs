using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Func func = new Func();
            //Console.WriteLine("Pasirinkite veiksmą:\n1. Generuoti skaičius - įrašyti: 1\n2. Rušiuoti aibę ir gauti rezultatus - įrašyti: 2\n3. Greitaveikos rezultatai - įrašyti: 3");
            //int veiksmas = int.Parse(Console.ReadLine());
            Console.WriteLine("Pasirinkite norimą elementų skaičių:");
            int ElSk = int.Parse(Console.ReadLine());
            func.HeapSortRAM(ElSk);
            func.HeapSortDISK(ElSk);
            Console.ReadKey();

        }
    }
}
