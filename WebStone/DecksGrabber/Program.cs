using DataAccess;
using Entity;
using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DecksGrabber
{
    class Program
    {
        static bool DeckParsed(DeckInfo deckInfo)
        {
            bool changed = true;

            lock (_syncObject)
            {
                using (var dbSession = _dbCore.OpenSession())
                {
                    var deck = new Deck();
                    
                    deck.Name = deckInfo.Name;
                    deck.Type = deckInfo.Type;
                    deck.Author = deckInfo.Author;
                    deck.Rating = deckInfo.Rating;
                    deck.ViewsCount = deckInfo.ViewsCount;
                    deck.CommentsCount = deckInfo.CommentsCount;
                    deck.Source = deckInfo.Source;
                    deck.Description = deckInfo.Description;
                    deck.Created = deckInfo.Created;
                    deck.Updated = deckInfo.Updated;
                    deck.AuthorUrl = deckInfo.AuthorUrl;
                    deck.DeckUrl = deckInfo.DeckUrl;

                    deck.Class = (PlayerClass)Enum.Parse(typeof(PlayerClass), deckInfo.Class);

                    List<string> cardsIds = new List<string>();

                    foreach (var card in deckInfo.Cards)
                    {
                        if (!_namesIdsPairs.ContainsKey(card))
                            throw new Exception("Cannot find ID for card " + card);

                        cardsIds.Add(_namesIdsPairs[card]);
                    }

                    deck.CardsIds = cardsIds;

                    dbSession.Store(deck);
                    dbSession.SaveChanges();
                }

                Console.WriteLine(deckInfo.Updated.ToShortDateString() + " : " + deckInfo.Class + " : " + deckInfo.Name);
            }

            return changed;
        }

        static void Main(string[] args)
        {
            try
            {
                // Initialise DB stuff
                _dbDocument = InitDbContext();
                _dbCore = new DatabaseCore(_dbDocument);

                // Load all cards
                using (var dbSession = _dbCore.OpenSession())
                {
                    using (var enumerator = dbSession.Advanced.Stream<Card>(fromEtag: Etag.Empty, start: 0, pageSize: int.MaxValue))
                    {
                        while (enumerator.MoveNext())
                        {
                            Card card = enumerator.Current.Document;

                            // Work only with default cards (not localized)
                            if (!card.Language.HasValue && !string.IsNullOrEmpty(card.Name))
                            {
                                _namesIdsPairs[card.Name] = card.Id;
                            }
                        }
                    }
                }

                var hearthPwdParser = new HearthpwnParser();
                hearthPwdParser.DeckParsedCallback = new DeckParsedDelegate(DeckParsed);
                hearthPwdParser.StartParsing(10);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.ToString());
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

        private static DocumentStore _dbDocument = null;
        private static DatabaseCore _dbCore = null;

        private static Dictionary<string, string> _namesIdsPairs = new Dictionary<string, string>();

        private static object _syncObject = new object();
    }
}
