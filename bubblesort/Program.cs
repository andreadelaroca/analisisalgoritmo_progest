using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 3;
            int[] array = new int[n];

            for (int i = 0; i <= n-2; i++)
            {
                for  (int j = 0; j <= n-i-2; j++)
                {
                    if(array[j] > array[j + 1])
                    {
                        int aux = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = aux;
                    }
                }
            }

            Stopwatch reloj = new Stopwatch();

            reloj.Start();
            reloj.Stop();
            Console.WriteLine("Arreglo ordenado: " + array);
            Console.WriteLine("Tiempo de ejecución: " + reloj.ElapsedMilliseconds + " ms");
        }
    }
}