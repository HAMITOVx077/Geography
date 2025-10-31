using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography
{
    public static class InputHelper
    {
        public static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;
                Console.Write("Ошибка! Введите число: ");
            }
        }

        public static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.Write("Ошибка! Введите целое число: ");
            }
        }
    }
}
