using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DataAccess;
using Newtonsoft.Json;
using Raven.Client.Document;

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

            var docStore = InitDbContext();
            var core = new DatabaseCore(docStore);

            using (var session = core.OpenSession())
            {
                foreach (var cardJsonModel in cards.Values.SelectMany(cardValues => cardValues))
                {
                    session.Store(cardJsonModel.Map());
                }

                session.SaveChanges();
            }
        }

        private static DocumentStore InitDbContext()
        {
            var documentStore = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["DatabaseUrl"],
                DefaultDatabase = ConfigurationManager.AppSettings["DefaultDatabase"]
            };

            documentStore.Initialize();

            return documentStore;
        }
    }
}
