using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.Write("Расположите фишки на столе, указав количество фишек через пробел: ");
            String[] elements = Console.ReadLine().Split(' ');

            int[] arr = new int[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                arr[i] = int.Parse(elements[i].ToString());
            }

            if ((arr.Sum() % arr.Length) != 0)
            {
                Console.WriteLine("Количество фишек указано неверно! Попробуйте еще раз.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("У каждого игрока должно быть по " + (arr.Sum() / arr.Length) + " фишек.");
            }
            
            Console.WriteLine("Минимальное количество необходимых перестановок:  " + Step(arr, 0)[0]);
            Console.ReadKey();

        }
        // Функция, считающая наименьшее расстояние между заданными
        // индексами массива при прямом и обратном обходе
        static ArrayList Distance(int x, int y, int len)
        {
            int res_forward = Math.Abs(x - y);
            int res_backward = len - res_forward;
            ArrayList res = new ArrayList();

            if (res_forward < res_backward)
            {
                res.Add("forward");
                res.Add(res_forward);
                return res;
            }
            else
            {
                res.Add("backward");
                res.Add(res_backward);
                return res;
            }
        }


        // Функция, осуществляющая оптимальную перестановку одной фишки
        // Возвращает количество сделанных перестановок
        static ArrayList Step(int[] arr, int res_shift_count = 0)
        {
            bool flag = false;
            while (flag == false)
            {
                int index_max = 0;
                int index_to_shift = 0;
                ArrayList max_arr = new ArrayList();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == arr.Max())
                    {
                        max_arr.Add(i);
                    }
                }
                
                ArrayList shifts = new ArrayList();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] < arr.Sum() / arr.Length)
                    {
                        shifts.Add(i);
                    }
                }

                int shift_dist = arr.Length;
                //char direction = new char();

                foreach (int elem in max_arr)
                {
                    foreach (int element in shifts)
                    {
                        if (Convert.ToInt32(Distance(elem, element, arr.Length)[1]) < shift_dist)
                        {
                            shift_dist = Convert.ToInt32(Distance(elem, element, arr.Length)[1]);
                            index_to_shift = element;
                            index_max = elem;
                        }
                    }
                }
                arr[index_max] -= 1;
                arr[index_to_shift] += 1;
                res_shift_count += shift_dist;

                // Цикл проверки равенства элементов массива среднему значению,
                // как только все элементы равны среднему, переменная flag 
                // меняет значение и происходит выход из while
                foreach (int i in arr)
                {
                    if (i != arr.Sum() / arr.Length)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        flag = true;
                        continue;
                    }
                }
            }
            ArrayList result = new ArrayList();
            result.Add(res_shift_count);
            return result;

        }
    }
}

