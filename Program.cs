using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10000;
            int Ui = 1;
            int p = 5087;
            int M = 2900;
            int length = 0;
            bool flag = true;
            double epsilon = 0.0001;
            double e_1 = 0;
            double e_2 = 0;
            double Ri = (double) Ui / p;
            List<double> values = new List<double>();
            for (int i = 0; i < n && i < int.MaxValue; i++)
            {
                values.Add(Ri);
                Ui = (Ui * M) % p;
                Ri = (double)Ui / p;
                if (i == 1)
                {
                    e_1 = Math.Abs(values[0] - values[1]);
                }
                if (i == 2)
                {
                    e_2 = Math.Abs(values[1] - values[2]);
                }
                if (i >= 1)
                {
                    double e1 = Math.Abs(values[i - 1] - values[i]);
                    double e2 = Math.Abs(values[i] - Ri);
                    if (flag && Math.Abs(e1 - e_1) <= epsilon && Math.Abs(e2 - e_2) <= epsilon)
                    {
                        length = i;
                        flag = false;
                    }
                }
            }
            double exp = Exp(values, n);
            double dis = Dis(values, n, exp);
            Console.WriteLine("Длина апериодичности: {0}", length); 
            Console.WriteLine("Мат. ожидание: {0}", exp);
            Console.WriteLine("Дисперсия: {0}", dis);
            Console.WriteLine("Частотные распределения:");
            Frequency(ref values);
            /*for (int i = 0; i < values.Count; i++)
            {
                Console.WriteLine(values[i]);
            }*/
        }

        public static double Exp(List<double> numbers, int n)
        {
            double sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum += numbers[i] / n;
            }
            return sum;
        }

        public static double Dis(List<double> numbers, int n, double exp)
        {
            double expVal = exp;
            double sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum += (numbers[i] - expVal) * (numbers[i] - expVal) / n;
            }
            return sum;
        }

        public static void Frequency(ref List<double> numbers)
        {
            List<double>[,] mas = new List<double>[10, 1];
            for (int i = 0; i < 10; i++)
            {
                mas[i, 0] = new List<double>();
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                int new_number = (int)(Math.Truncate(numbers[i] * 10));
                mas[new_number, 0].Add(1);
            }

            Console.WriteLine("0 - 1: {0}", mas[0, 0].Count);
            Console.WriteLine("1 - 2: {0}", mas[1, 0].Count);
            Console.WriteLine("2 - 3: {0}", mas[2, 0].Count);
            Console.WriteLine("3 - 4: {0}", mas[3, 0].Count);
            Console.WriteLine("4 - 5: {0}", mas[4, 0].Count);
            Console.WriteLine("5 - 6: {0}", mas[5, 0].Count);
            Console.WriteLine("6 - 7: {0}", mas[6, 0].Count);
            Console.WriteLine("7 - 8: {0}", mas[7, 0].Count);
            Console.WriteLine("8 - 9: {0}", mas[8, 0].Count);
        }
    }
}
