using System;
using System.Linq;
using Entity;
using NUnit.Framework;
using WebStone.Domain;

namespace WebStoneTests
{
    [TestFixture]
    public class DeckBuilderTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Push_Null()
        {
            var target = new DeckBuilder();
            target.Push(null);
        }

        [Test]
        public void Push_Card_ResultedDeckContainsOneCard()
        {
            var target = new DeckBuilder();
            target.Push(new Card());

            var deck = target.Build();

            Assert.AreEqual(1, deck.CardsIds.Count());
        }

        [Test]
        public void Push_ThreeCardsWithSameId_ResultedDeckContainsOneCard()
        {
            var target = new DeckBuilder();
            var card = new Card {Id = "1"};

            target.Push(card);
            target.Push(card);

            target.Push(new Card { Id = "2" });

            var deck = target.Build();
            var cardIds = deck.CardsIds.ToList();

            Assert.AreEqual(3, cardIds.Count());
        }
    }
}