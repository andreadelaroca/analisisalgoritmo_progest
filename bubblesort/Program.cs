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

            // Ejecutar análisis para los tres casos del algoritmo Bubble Sort
            // Mejor caso: arreglo ya ordenado (ascendente)
            EjecutarCaso("Mejor caso (ordenado)", GenerarMejorCaso(n));

            // Caso promedio: arreglo aleatorio
            EjecutarCaso("Caso promedio (aleatorio)", GenerarCasoPromedio(n));

            // Peor caso: arreglo ordenado inversamente (descendente)
            EjecutarCaso("Peor caso (inverso)", GenerarPeorCaso(n));
        }

        // Ejecuta Bubble Sort sobre un arreglo, muestra arreglo si es pequeño
        // y reporta métricas útiles para el análisis del algoritmo.
        static void EjecutarCaso(string titulo, int[] array)
        {
            Console.WriteLine($"\n=== {titulo} ===");

            if (array.Length <= 30)
            {
                Console.WriteLine("Arreglo inicial: " + string.Join(", ", array));
            }

            var (comparaciones, intercambios, tiempoMs) = BubbleSort(array);
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

        // Genera el mejor caso para Bubble Sort: arreglo ya ordenado ascendente.
        static int[] GenerarMejorCaso(int n)
        {
            // Ejemplo simple: 0, 1, 2, ..., n-1
            return Enumerable.Range(0, n).ToArray();
        }

        // Genera un caso promedio: arreglo con valores aleatorios.
        static int[] GenerarCasoPromedio(int n)
        {
            var rand = new Random();
            return Enumerable.Range(0, n).Select(_ => rand.Next(0, 1000)).ToArray();
        }

        // Genera el peor caso: arreglo ordenado de forma descendente.
        static int[] GenerarPeorCaso(int n)
        {
            // Ejemplo: n-1, n-2, ..., 0
            return Enumerable.Range(0, n).Select(i => n - 1 - i).ToArray();
        }

        // Algoritmo Bubble Sort clásico sin "early-exit".
        // Retorna las métricas para el análisis: comparaciones, intercambios, tiempo en ms.
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