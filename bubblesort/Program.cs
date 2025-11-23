using System;
using System.Diagnostics;
using System.Linq;

namespace BubbleSort
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

            // Mostrar el arreglo sólo si es pequeño para no saturar el output
            if (n <= 30)
            {
                Console.WriteLine("Arreglo inicial: " + string.Join(", ", array));
            }

            // Ejecutar el ordenamiento y obtener métricas
            var (comparaciones, intercambios, tiempoMs) = BubbleSort(array);

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

        // Función BubbleSort, retorna las métricas para el análisis
        static (long comparaciones, long intercambios, long tiempoMs) BubbleSort(int[] arr)
        {
            int n = arr.Length;
            long comparaciones = 0;
            long intercambios = 0;
            var sw = Stopwatch.StartNew();

            // Se recorre el arreglo n-1 veces
            for (int i = 0; i < n - 1; i++)
            {
                // En cada iteración se comparan pares adyacentes
                for (int j = 0; j < n - i - 1; j++)
                {
                    comparaciones++;
                    if (arr[j] > arr[j + 1])
                    {
                        // Intercambio de elementos
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                        intercambios++;
                    }
                }
            }

            sw.Stop();
            return (comparaciones, intercambios, sw.ElapsedMilliseconds);
        }

        // Comprueba si el arreglo está ordenado de forma ascendente
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