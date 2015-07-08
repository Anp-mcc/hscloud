using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace DumpLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
                Console.WriteLine("There is no any arguments");

            var jsonStr = File.ReadAllText(args[0]);

            var cards = JsonConvert.DeserializeObject<Dictionary<string, List<CardJsonModel>>>(jsonStr);

            foreach (var cardValues in cards.Values)
            {
                foreach (var cardJsonModel in cardValues)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(cardJsonModel))
                    {
                        var name = descriptor.Name;
                        var value = descriptor.GetValue(cardJsonModel);

                        if (value is IEnumerable<string>)
                            value = string.Join(" ", (IEnumerable<string>) value);

                        Console.WriteLine("{0}={1}", name, value);
                    }
                }
            }

        }
    }
}
