using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geography
{
    public class GeoCalc
    {
        //Модуль 1: Конвертер координат

        //конвертирует градусы в радианы
        public double DegreesToRadians(double degrees)
        {
            return degrees * Constants.DegreesToRadians;
        }

        //вычисляет расстояние между двумя точками по формуле гаверсинуса
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            //конвертируем координаты из градусов в радианы
            double lat1Rad = DegreesToRadians(lat1);
            double lon1Rad = DegreesToRadians(lon1);
            double lat2Rad = DegreesToRadians(lat2);
            double lon2Rad = DegreesToRadians(lon2);

            //вычисляем разницу координат
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            //вычисляем расстояние по формуле гаверсинуса
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                      Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                      Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //возвращаем расстояние в километрах
            return Constants.EarthRadiusKm * c;
        }

        //вычисляет азимут от первой точки ко второй
        public double CalculateAzimuth(double x1, double y1, double x2, double y2)
        {
            //вычисляем разницу координат
            double dx = x2 - x1;
            double dy = y2 - y1;

            //вычисляем угол в радианах
            double angle = Math.Atan2(dy, dx);

            //корректируем угол, если он отрицательный
            if (angle < 0) angle += 2 * Math.PI;

            //конвертируем радианы в градусы
            return angle * Constants.RadiansToDegrees;
        }

        //Модуль 2: Конвертер единиц

        //конвертирует значения между различными единицами измерения
        public double ConvertDistance(double value, string from, string to)
        {
            //словарь с коэффициентами конвертации
            var conversions = new Dictionary<string, Dictionary<string, double>>
            {
                ["km"] = new Dictionary<string, double> { ["miles"] = Constants.KilometersToMiles },
                ["miles"] = new Dictionary<string, double> { ["km"] = Constants.MilesToKilometers },
                ["meters"] = new Dictionary<string, double> { ["feet"] = Constants.MetersToFeet },
                ["feet"] = new Dictionary<string, double> { ["meters"] = Constants.FeetToMeters },
                ["hectares"] = new Dictionary<string, double> { ["acres"] = Constants.HectaresToAcres },
                ["acres"] = new Dictionary<string, double> { ["hectares"] = Constants.AcresToHectares }
            };

            //проверяем возможность конвертации и выполняем её
            if (conversions.ContainsKey(from) && conversions[from].ContainsKey(to))
            {
                return value * conversions[from][to];
            }

            //возвращаем исходное значение, если конвертация невозможна
            return value;
        }

        //Методы меню

        //главное меню приложения
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== ГЕОГРАФИЧЕСКИЙ КАЛЬКУЛЯТОР ===");
                Console.WriteLine("1. Перевод градусов в радианы");
                Console.WriteLine("2. Расчет расстояния между двумя точками на карте");
                Console.WriteLine("3. Расчет азимута (направления) между точками");
                Console.WriteLine("4. Конвертер единиц измерения");
                Console.WriteLine("5. Выход из программы");
                Console.WriteLine("=====================================");

                int choice = InputHelper.ReadInt("Выберите опцию (1-5): ");

                switch (choice)
                {
                    case 1: ConvertDegreesToRadians(); break;
                    case 2: CalculateDistanceMenu(); break;
                    case 3: CalculateAzimuthMenu(); break;
                    case 4: ConvertUnitsMenu(); break;
                    case 5:
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор! Пожалуйста, выберите опцию от 1 до 5.");
                        break;
                }
            }
        }

        //меню конвертации градусов в радианы
        private void ConvertDegreesToRadians()
        {
            Console.WriteLine("\n--- КОНВЕРТАЦИЯ ГРАДУСОВ В РАДИАНЫ ---");
            double degrees = InputHelper.ReadDouble("Введите значение в градусах: ");
            double radians = DegreesToRadians(degrees);
            Console.WriteLine($"Результат: {degrees}° = {radians:F6} радиан");
        }

        //меню расчета расстояния между точками
        private void CalculateDistanceMenu()
        {
            Console.WriteLine("\n--- РАСЧЕТ РАССТОЯНИЯ МЕЖДУ ТОЧКАМИ ---");
            Console.WriteLine("Введите координаты в десятичных градусах:");
            Console.WriteLine("Пример: Москва - 55.7558, 37.6173");
            Console.WriteLine("Пример: Санкт-Петербург - 59.9343, 30.3351");

            double lat1 = InputHelper.ReadDouble("Широта первой точки (например, 55.7558): ");
            double lon1 = InputHelper.ReadDouble("Долгота первой точки (например, 37.6173): ");
            double lat2 = InputHelper.ReadDouble("Широта второй точки (например, 59.9343): ");
            double lon2 = InputHelper.ReadDouble("Долгота второй точки (например, 30.3351): ");

            double distance = CalculateDistance(lat1, lon1, lat2, lon2);
            Console.WriteLine($"\nРасстояние между точками: {distance:F2} км");
            Console.WriteLine($"Это примерно {distance * 1000:F0} метров");
        }

        //меню расчета азимута
        private void CalculateAzimuthMenu()
        {
            Console.WriteLine("\n--- РАСЧЕТ АЗИМУТА МЕЖДУ ТОЧКАМИ ---");
            Console.WriteLine("Азимут - это угол между направлением на север и направлением на объект");
            Console.WriteLine("Введите координаты точек:");

            double x1 = InputHelper.ReadDouble("Координата X первой точки: ");
            double y1 = InputHelper.ReadDouble("Координата Y первой точки: ");
            double x2 = InputHelper.ReadDouble("Координата X второй точки: ");
            double y2 = InputHelper.ReadDouble("Координата Y второй точки: ");

            double azimuth = CalculateAzimuth(x1, y1, x2, y2);
            Console.WriteLine($"\nАзимут от первой точки ко второй: {azimuth:F2}°");

            //дополнительная информация о направлении
            if (azimuth >= 337.5 || azimuth < 22.5)
                Console.WriteLine("Направление: Север");
            else if (azimuth >= 22.5 && azimuth < 67.5)
                Console.WriteLine("Направление: Северо-Восток");
            else if (azimuth >= 67.5 && azimuth < 112.5)
                Console.WriteLine("Направление: Восток");
            else if (azimuth >= 112.5 && azimuth < 157.5)
                Console.WriteLine("Направление: Юго-Восток");
            else if (azimuth >= 157.5 && azimuth < 202.5)
                Console.WriteLine("Направление: Юг");
            else if (azimuth >= 202.5 && azimuth < 247.5)
                Console.WriteLine("Направление: Юго-Запад");
            else if (azimuth >= 247.5 && azimuth < 292.5)
                Console.WriteLine("Направление: Запад");
            else
                Console.WriteLine("Направление: Северо-Запад");
        }

        //меню конвертации единиц измерения
        private void ConvertUnitsMenu()
        {
            Console.WriteLine("\n--- КОНВЕРТЕР ЕДИНИЦ ИЗМЕРЕНИЯ ---");
            Console.WriteLine("Доступные единицы измерения:");
            Console.WriteLine("Расстояние: km (километры), miles (мили), meters (метры), feet (футы)");
            Console.WriteLine("Площадь: hectares (гектары), acres (акры)");

            double value = InputHelper.ReadDouble("Введите значение для конвертации: ");
            string from = InputHelper.ReadString("Из какой единицы (km/miles/meters/feet/hectares/acres): ");
            string to = InputHelper.ReadString("В какую единицу (km/miles/meters/feet/hectares/acres): ");

            double result = ConvertDistance(value, from, to);
            Console.WriteLine($"\nРезультат: {value} {from} = {result:F4} {to}");

            //дополнительная справочная информация
            if (from == "km" && to == "miles")
                Console.WriteLine("Справка: 1 километр ≈ 0.621371 мили");
            else if (from == "miles" && to == "km")
                Console.WriteLine("Справка: 1 миля ≈ 1.60934 километра");
        }
    }
}