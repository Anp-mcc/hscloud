using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DataAccess;
using Entity;
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
                var cardJsonModel = cards.Values.SelectMany(cardValues => cardValues);
                var ids = cardJsonModel.Take(30).Select(x => x.Id);

                var deck = new Deck() {CardsIds = ids, Name = "SomeDeck", Hero = HeroClass.Hunter};
                session.Store(deck);

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
