using System;
using System.Collections.Generic;
using System.Linq;
using CQS;
using CQS.Query;
using DataAccess;
using Entity;
using Moq;
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
            using (InitCore())
            {
                _target.Retrieve(null);
            }
        }

        [Test]
        public void Retrieve_DecksInDb_CardsCountMatch()
        {
            using (var store = NewRemoteDocumentStore())
            {
                using (var session = store.OpenSession())
                {
                    session.Store(new Deck() { Name = "Some", 
                                                CardsIds = new List<string> 
                                                {   
                                                    "1",
                                                    "2"
                                                }});
                    session.SaveChanges();

                    var decks = session.Query<Deck>().ToList();
                }

                var core = new DatabaseCore(store);
                _target = new DeckWithCardsQueryHandler(core);

                var result = _target.Retrieve(new DeckWithCardsQuery());

                Assert.AreEqual(2, result.CardNames.Count());
            }
        }


        private EmbeddableDocumentStore InitCore()
        {
            var store = NewDocumentStore();
            store.Initialize();
            var core = new Mock<IDatabaseCore>();
            core.Setup(x => x.OpenSession()).Returns(store.OpenSession);
            _target = new DeckWithCardsQueryHandler(core.Object);

            return store;
        }
    }
}