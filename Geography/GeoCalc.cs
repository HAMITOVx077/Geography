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
            double angle = Math.Atan2(dy, dx);
            if (angle < 0) angle += 2 * Math.PI;
            return angle * Constants.RadiansToDegrees;
        }

        // Модуль 2: Конвертер единиц

        public double ConvertDistance(double value, string from, string to)
        {
            var conversions = new Dictionary<string, Dictionary<string, double>>
            {
                ["km"] = new Dictionary<string, double> { ["miles"] = Constants.KilometersToMiles },
                ["miles"] = new Dictionary<string, double> { ["km"] = Constants.MilesToKilometers },
                ["meters"] = new Dictionary<string, double> { ["feet"] = Constants.MetersToFeet },
                ["feet"] = new Dictionary<string, double> { ["meters"] = Constants.FeetToMeters },
                ["hectares"] = new Dictionary<string, double> { ["acres"] = Constants.HectaresToAcres },
                ["acres"] = new Dictionary<string, double> { ["hectares"] = Constants.AcresToHectares }
            };

            if (conversions.ContainsKey(from) && conversions[from].ContainsKey(to))
            {
                return value * conversions[from][to];
            }

            return value;
        }

        // Методы меню

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Географический калькулятор ===");
                Console.WriteLine("1. Градусы в радианы");
                Console.WriteLine("2. Расстояние между точками");
                Console.WriteLine("3. Азимут");
                Console.WriteLine("4. Конвертер единиц");
                Console.WriteLine("5. Выход");

                int choice = InputHelper.ReadInt("Выберите опцию: ");

                switch (choice)
                {
                    case 1: ConvertDegreesToRadians(); break;
                    case 2: CalculateDistanceMenu(); break;
                    case 3: CalculateAzimuthMenu(); break;
                    case 4: ConvertUnitsMenu(); break;
                    case 5: return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
            }
        }

        private void ConvertDegreesToRadians()
        {
            double degrees = InputHelper.ReadDouble("Введите градусы: ");
            double radians = DegreesToRadians(degrees);
            Console.WriteLine($"{degrees}° = {radians:F6} рад");
        }

        private void CalculateDistanceMenu()
        {
            double lat1 = InputHelper.ReadDouble("Широта точки 1: ");
            double lon1 = InputHelper.ReadDouble("Долгота точки 1: ");
            double lat2 = InputHelper.ReadDouble("Широта точки 2: ");
            double lon2 = InputHelper.ReadDouble("Долгота точки 2: ");

            double distance = CalculateDistance(lat1, lon1, lat2, lon2);
            Console.WriteLine($"Расстояние: {distance:F2} км");
        }

        private void CalculateAzimuthMenu()
        {
            double x1 = InputHelper.ReadDouble("X1: ");
            double y1 = InputHelper.ReadDouble("Y1: ");
            double x2 = InputHelper.ReadDouble("X2: ");
            double y2 = InputHelper.ReadDouble("Y2: ");

            double azimuth = CalculateAzimuth(x1, y1, x2, y2);
            Console.WriteLine($"Азимут: {azimuth:F2}°");
        }

        private void ConvertUnitsMenu()
        {
            double value = InputHelper.ReadDouble("Значение: ");
            string from = InputHelper.ReadString("Из (km/miles/meters/feet/hectares/acres): ");
            string to = InputHelper.ReadString("В (km/miles/meters/feet/hectares/acres): ");

            double result = ConvertDistance(value, from, to);
            Console.WriteLine($"Результат: {result:F4}");
        }
    }
}
