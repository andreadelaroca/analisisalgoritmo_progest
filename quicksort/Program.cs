using System;
using System.Diagnostics;
using System.Linq;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tamaño por defecto
            int n = 1000;

            // Leer tamaño desde argumento o entrada estándar
            if (args.Length > 0)
            {
                if (!int.TryParse(args[0], out n) || n < 0) n = 1000;
            }
            else
            {
                Console.Write("Ingrese tamaño del arreglo (enter para 1000): ");
                string input = Console.ReadLine() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(input) && (!int.TryParse(input, out n) || n < 0))
                {
                    Console.WriteLine("Entrada inválida. Se usará n = 1000");
                    n = 1000;
                }
            }

            // Generar arreglo aleatorio
            var rand = new Random();
            int[] array = Enumerable.Range(0, n).Select(_ => rand.Next(0, 1000)).ToArray();

            // Mostrar el arreglo sólo si es pequeño
            if (n <= 30)
            {
                Console.WriteLine("Arreglo inicial: " + string.Join(", ", array));
            }

            // Ejecutar QuickSort y obtener métricas
            var (comparaciones, intercambios, tiempoMs) = QuickSort(array);

            // Verificar que quedó ordenado
            bool ordenado = Ordenado(array);

            if (n <= 30)
            {
                Console.WriteLine("Arreglo ordenado: " + string.Join(", ", array));
            }

            // Mostrar métricas
            Console.WriteLine($"Tamaño del arreglo: {n}");
            Console.WriteLine($"Comparaciones: {comparaciones}");
            Console.WriteLine($"Intercambios: {intercambios}");
            Console.WriteLine($"Tiempo de ejecución: {tiempoMs} ms");
            Console.WriteLine($"¿Se ordenó correctamente?: {ordenado}");
        }

        // Función QuickSort con métricas
        static (long comparaciones, long intercambios, long tiempoMs) QuickSort(int[] arr)
        {
            long comparaciones = 0;
            long intercambios = 0;
            var sw = Stopwatch.StartNew();

            void Sort(int[] a, int low, int high)
            {
                if (low < high)
                {
                    int p = Partition(a, low, high);
                    Sort(a, low, p - 1);
                    Sort(a, p + 1, high);
                }
            }

            int Partition(int[] a, int low, int high)
            {
                int pivot = a[high];
                int i = low - 1;

                for (int j = low; j < high; j++)
                {
                    comparaciones++;
                    if (a[j] <= pivot)
                    {
                        i++;
                        (a[i], a[j]) = (a[j], a[i]);
                        intercambios++;
                    }
                }

                (a[i + 1], a[high]) = (a[high], a[i + 1]);
                intercambios++;
                return i + 1;
            }

            Sort(arr, 0, arr.Length - 1);

            sw.Stop();
            return (comparaciones, intercambios, sw.ElapsedMilliseconds);
        }

        // Comprueba si el arreglo está ordenado
        static bool Ordenado(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i]) return false;
            }
            return true;
        }
    }
}
