using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography
{
    public class GeoCalc
    {
        // Модуль 1: Конвертер координат

        public double DegreesToRadians(double degrees)
        {
            return degrees * Constants.DegreesToRadians;
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                      Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                      Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return Constants.EarthRadiusKm * c;
        }

        public double CalculateAzimuth(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double a = Math.Atan2(dy, dx);
            if (a < 0) a += 2 * Math.PI;
            return a * 57.29577951308232;
        }

        // Модуль 2: Конвертер единиц

        public double ConvertDistance(double value, string from, string to)
        {
            if (from == "km" && to == "miles")
                return value * 0.621371;
            else if (from == "miles" && to == "km")
                return value * 1.60934;
            else if (from == "meters" && to == "feet")
                return value * 3.28084;
            else if (from == "feet" && to == "meters")
                return value * 0.3048;
            else if (from == "hectares" && to == "acres")
                return value * 2.47105;
            else if (from == "acres" && to == "hectares")
                return value * 0.404686;
            else
                return value;
        }

        public void DoEverything()
        {
            Console.WriteLine("Конвертер координат и единиц");
            Console.WriteLine("1. Перевод градусов в радианы");
            Console.WriteLine("2. Расчет расстояния");
            Console.WriteLine("3. Определение азимута");
            Console.WriteLine("4. Конвертер единиц");

            string input = Console.ReadLine();
            int choice = int.Parse(input);

            if (choice == 1)
            {
                Console.Write("Введите градусы: ");
                double deg = double.Parse(Console.ReadLine());
                double rad = ConvertToRadians(deg);
                Console.WriteLine($"Результат: {rad}");
            }
            else if (choice == 2)
            {
                Console.Write("Введите lat1: ");
                double lat1 = double.Parse(Console.ReadLine());
                Console.Write("Введите lon1: ");
                double lon1 = double.Parse(Console.ReadLine());
                Console.Write("Введите lat2: ");
                double lat2 = double.Parse(Console.ReadLine());
                Console.Write("Введите lon2: ");
                double lon2 = double.Parse(Console.ReadLine());
                double dist = CalculateDistance(lat1, lon1, lat2, lon2);
                Console.WriteLine($"Расстояние: {dist} км");
            }
            else if (choice == 3)
            {
                Console.Write("Введите x1: ");
                double x1 = double.Parse(Console.ReadLine());
                Console.Write("Введите y1: ");
                double y1 = double.Parse(Console.ReadLine());
                Console.Write("Введите x2: ");
                double x2 = double.Parse(Console.ReadLine());
                Console.Write("Введите y2: ");
                double y2 = double.Parse(Console.ReadLine());
                double az = CalculateAzimuth(x1, y1, x2, y2);
                Console.WriteLine($"Азимут: {az}°");
            }
            else if (choice == 4)
            {
                Console.Write("Введите значение: ");
                double val = double.Parse(Console.ReadLine());
                Console.Write("Из (km/miles/meters/feet/hectares/acres): ");
                string from = Console.ReadLine();
                Console.Write("В (km/miles/meters/feet/hectares/acres): ");
                string to = Console.ReadLine();
                double result = ConvertDistance(val, from, to);
                Console.WriteLine($"Результат: {result}");
            }
        }
    }
}
