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

            // Ejecutar análisis de QuickSort para tres casos típicos
            EjecutarCaso("Mejor caso (pivot ideal)", GenerarMejorCaso(n));
            EjecutarCaso("Caso promedio (aleatorio)", GenerarCasoPromedio(n));
            EjecutarCaso("Peor caso (ordenado/inverso con pivot extremo)", GenerarPeorCaso(n));
        }

        // Ejecuta QuickSort sobre el arreglo dado, imprime (si es pequeño)
        // y muestra métricas para el análisis del algoritmo.
        static void EjecutarCaso(string titulo, int[] array)
        {
            Console.WriteLine($"\n=== {titulo} ===");
            if (array.Length <= 30)
            {
                Console.WriteLine("Arreglo inicial: " + string.Join(", ", array));
            }

            var (comparaciones, intercambios, tiempoMs) = QuickSort(array);
            bool ordenado = Ordenado(array);

            if (array.Length <= 30)
            {
                Console.WriteLine("Arreglo ordenado: " + string.Join(", ", array));
            }

            Console.WriteLine($"Tamaño del arreglo: {array.Length}");
            Console.WriteLine($"Comparaciones: {comparaciones}");
            Console.WriteLine($"Intercambios: {intercambios}");
            Console.WriteLine($"Tiempo de ejecución: {tiempoMs} ms");
            Console.WriteLine($"¿Se ordenó correctamente?: {ordenado}");
        }

        // Genera el mejor caso para QuickSort con pivot final (a[high]) como en esta implementación.
        // Aquí el mejor caso se logra si el pivot escoge particiones balanceadas: típicamente un arreglo aleatorio ya
        // suele producir comportamiento promedio cercano al mejor; sin embargo, para un pivot fijo (último elemento),
        // podemos construir un arreglo con el pivot en la mediana repetida por bloques para inducir particiones más parejas.
        static int[] GenerarMejorCaso(int n)
        {
            if (n <= 1) return Enumerable.Range(0, n).ToArray();
            // Aproximación práctica: arreglo aleatorio (suele dar particiones relativamente balanceadas)
            // y aseguramos que el último elemento sea aproximadamente mediana.
            var rand = new Random();
            int[] arr = Enumerable.Range(0, n).Select(_ => rand.Next(0, 1000)).ToArray();
            Array.Sort(arr); // ordenar
            int median = arr[n / 2];
            arr[n - 1] = median; // colocar mediana como pivot inicial
            // mezclar parcialmente para no dejarlo totalmente ordenado
            for (int i = 0; i < n - 2; i += 2)
            {
                (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
            }
            return arr;
        }

        // Genera el caso promedio: arreglo aleatorio uniforme.
        static int[] GenerarCasoPromedio(int n)
        {
            var rand = new Random();
            return Enumerable.Range(0, n).Select(_ => rand.Next(0, 1000)).ToArray();
        }

        // Genera el peor caso para esta variante (pivot = último elemento):
        // Arreglo ya ordenado ascendentemente o descendentemente, lo que causa particiones muy desbalanceadas O(n^2).
        static int[] GenerarPeorCaso(int n)
        {
            // Usamos ascendente para que el pivot (último) siempre sea el mayor y deje subproblemas de tamaño n-1.
            return Enumerable.Range(0, n).ToArray();
        }

        // QuickSort Lomuto (pivot = a[high]) con métricas de comparaciones e intercambios.
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
