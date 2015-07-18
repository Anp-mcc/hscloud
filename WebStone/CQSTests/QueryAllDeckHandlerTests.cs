using System;
using System.Linq;
using CQS;
using CQS.Query;
using CQS.QueryHandler;
using DataAccess;
using Entity;
using Moq;
using NUnit.Framework;
using Raven.Client.Embedded;
using Raven.Tests.Helpers;

namespace CQSTests
{
    [TestFixture]
    public class QueryAllDeckHandlerTests : RavenTestBase
    {
        private AllDeckQueryHandler _target;

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Retrieve_Null_ArgumentException()
        {
            using (var store = new EmbeddableDocumentStore())
            {
                store.Initialize();

                _target = new AllDeckQueryHandler(new DatabaseCore(store));
                _target.Retrieve(null);
            }
        }

        [Test]
        public void Retrieve_DecksInDb_CountMatch()
        {
            using (var store = new EmbeddableDocumentStore())
            {
                store.Initialize();

                var core = new DatabaseCore(store);
                using (var session = store.OpenSession())
                {
                    session.Store(new Deck
                    {
                        Name = "OneStuff",
                    });
                    session.SaveChanges();
                }


                _target = new AllDeckQueryHandler(core);
                var result = _target.Retrieve(new AllDeckQuery());

                Assert.AreEqual(1, result.Decks.Count());
            }
        }
    }
}
