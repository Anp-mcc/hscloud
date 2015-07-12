using System;
using System.Linq;
using CQS;
using CQS.Query;
using DataAccess;
using Entity;
using Moq;
using NUnit.Framework;
using Raven.Client.Document;
using Raven.Tests.Helpers;

namespace CQSTests
{
    [TestFixture]
    public class QueryAllDeckHandlerTests : RavenTestBase
    {
        private QueryAllDeckHandler _target;

        private Mock<IDatabaseCore> _core;
        private DocumentStore _store;

        public QueryAllDeckHandlerTests()
        {
            
            _core = new Mock<IDatabaseCore>();
            _core.Setup(x => x.OpenSession()).Returns(_store.OpenSession);

            _target = new QueryAllDeckHandler(_core.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Retrieve_Null_ArgumentException()
        {
            _target.Retrieve(null);
        }

        [Test]
        public void Retrieve_DecksInDb_CountMatch()
        {
            using (var core = InitCore())
            {
                using (var session = core.OpenSession())
                {
                    session.Store(new Deck());
                    session.SaveChanges();
                }
            }

            var result = _target.Retrieve(new QueryAllDeck());

            Assert.AreEqual(1, result.Decks.Count());
        }

        private DatabaseCore InitCore()
        {
            var store = NewRemoteDocumentStore();
            store.Initialize();
            return new DatabaseCore(store);
        }
    }
}
