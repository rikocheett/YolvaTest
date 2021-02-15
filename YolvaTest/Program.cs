using System;
using GeoTest.Classes;

namespace GeoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите адрес:");
            string address = Console.ReadLine();

            Console.WriteLine("Введите частоту точек:");
            int rate = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Введите имя файла без расширения:");
            string fileName = Console.ReadLine();

            GeoOSM geo = new GeoOSM(address, fileName, rate);
            geo.DoRequest();
        }
    }
}
