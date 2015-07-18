using System;
using System.Collections.Generic;
using System.Linq;
using CQS.Query;
using CQS.QueryHandler;
using DataAccess;
using Entity;
using NUnit.Framework;
using Raven.Client.Embedded;
using Raven.Tests.Helpers;

namespace CQSTests
{
    [TestFixture]
    public class DeckWithCardsQueryHandlerTests : RavenTestBase
    {
        private DeckWithCardsQueryHandler _target;

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Retrieve_Null_ArgumentException()
        {
            _target.Retrieve(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Retrieve_DeckInDb_CardIsNotFound()
        {
            using (var store = new EmbeddableDocumentStore() { RunInMemory = true })
            {
                store.Initialize();

                var core = new DatabaseCore(store);
                using (var session = store.OpenSession())
                {
                    session.Store(new Deck
                    {
                        Name = "OneStuff",
                        CardsIds = new List<string>
                        {
                            "1",
                            "2"
                        }
                    });

                    session.SaveChanges();
                }

                _target = new DeckWithCardsQueryHandler(core);

                _target.Retrieve(new DeckWithCardsQuery());
            }
        }

        [Test]
        public void Retrieve_DecksInDb_NoParameter_FirstDeck()
        {
            using (var store = new EmbeddableDocumentStore() { RunInMemory = true })
            {
                store.Initialize();

                var core = new DatabaseCore(store);
                using (var session = store.OpenSession())
                {
                    session.Store(new Deck
                    {
                        Name = "OneStuff",
                        CardsIds = new List<string> 
                                                {   
                                                    "1",
                                                    "2"
                                                }
                    });

                    session.Store(new Card() { Id = "1", Name = "Firt" });
                    session.Store(new Card() { Id = "2", Name = "Second" });

                    session.SaveChanges();
                }

                _target = new DeckWithCardsQueryHandler(core);

                var result = _target.Retrieve(new DeckWithCardsQuery());

                Assert.AreEqual(2, result.CardNames.Count());
            }
        }

        [Test]
        public void Retrieve_DecksInDb_NoCardsInDeck_DeckWithEmptyListOfNmaes()
        {
            using (var store = new EmbeddableDocumentStore() { RunInMemory = true })
            {
                store.Initialize();

                var core = new DatabaseCore(store);
                using (var session = store.OpenSession())
                {
                    session.Store(new Deck
                    {
                        Name = "Stuff",
                    });
                    session.SaveChanges();
                }

                _target = new DeckWithCardsQueryHandler(core);

                var result = _target.Retrieve(new DeckWithCardsQuery());

                Assert.IsFalse(result.CardNames.Any());
            }
        }
    }
}