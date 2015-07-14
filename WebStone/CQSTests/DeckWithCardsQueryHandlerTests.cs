using System;
using System.Collections.Generic;
using System.Linq;
using CQS;
using CQS.Query;
using CQS.QueryHandler;
using DataAccess;
using Entity;
using Moq;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Linq;
using Raven.Tests.Helpers;

namespace CQSTests
{
    [TestFixture]
    public class DeckWithCardsQueryHandlerTests : RavenTestBase
    {
        private DeckWithCardsQueryHandler _target;
        private Mock<IDocumentSession> _session;

        public DeckWithCardsQueryHandlerTests()
        {
            _session = new Mock<IDocumentSession>();
            var core = new Mock<IDatabaseCore>();
            core.Setup(x => x.OpenSession()).Returns(_session.Object);
            _target = new DeckWithCardsQueryHandler(core.Object);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Retrieve_Null_ArgumentException()
        {
            _target.Retrieve(null);
        }

        [Test]
        public void Retrieve_DecksInDb_NoParameter_FirstDeck()
        {
            var deck = new Deck() { Name = "Some", 
                                                CardsIds = new List<string> 
                                                {   
                                                    "1",
                                                    "2"
                                                }};

            //_session.Setup(x => x.Query<Deck>()).Returns(new Func<IRavenQueryable<Deck>>);
            _session.Setup(x => x.Load<Card>("")).Returns(new Card());

            var result = _target.Retrieve(new DeckWithCardsQuery());

            Assert.AreEqual(2, result.CardNames.Count());
        }
    }
}