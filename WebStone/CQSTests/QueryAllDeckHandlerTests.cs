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
            using (InitCore())
            {
                _target.Retrieve(null);
            }
        }

        [Test]
        public void Retrieve_DecksInDb_CountMatch()
        {
            using (var core = InitCore())
            {
                using (var session = core.OpenSession())
                {
                    session.Store(new Deck(){Name = "Some"});
                    session.SaveChanges();
                }
            
                var result = _target.Retrieve(new AllDeckQuery());

                Assert.AreEqual(1, result.Decks.Count());
            }
        }

        private EmbeddableDocumentStore InitCore()
        {
            var store = new EmbeddableDocumentStore() { DefaultDatabase = "TestDb"};
            store.Initialize();
            var core = new Mock<IDatabaseCore>();
            core.Setup(x => x.OpenSession()).Returns(store.OpenSession);
            _target = new AllDeckQueryHandler(core.Object);

            return store;
        }
    }
}
