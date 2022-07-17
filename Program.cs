using System;
using Exercise_Lambda_LINQ.Entities;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Exercise_Lambda_LINQ {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter file path: ");
            string path = Console.ReadLine();

            List<Product> products = new List<Product>();

            try {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] v = sr.ReadLine().Split(',');
                        string name = v[0];
                        double price = double.Parse(v[1], CultureInfo.InvariantCulture);
                        products.Add(new Product(name, price));

                    }
                }

                double avg = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("Average price: " + avg.ToString("F2", CultureInfo.InvariantCulture));

                var names = products.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
                foreach (string name in names) {
                    Console.WriteLine(name);
                }
            }
            catch(IOException e) {
                Console.WriteLine("ERROR" + e.Message);
            }


    }
    }
}
