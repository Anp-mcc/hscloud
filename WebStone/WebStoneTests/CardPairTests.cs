using System;
using NUnit.Framework;
using WebStone.Domain;

namespace WebStoneTests
{
    [TestFixture]
    public class CardPairTests
    {

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CardPair_Null_Exception()
        {
            new CardPair(null);
        }

        [Test]
        public void CardPair_CountShouldBeOne()
        {
            var cardPair = new CardPair("1");

            Assert.AreEqual(1, cardPair.Count);
        }

        [Test]
        public void CardPair_CardId_AreSameAsCard()
        {
            var pair = new CardPair("1");

            Assert.AreEqual("1", pair.Id);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_CardWithDifferentId()
        {
            var pair = new CardPair("1");

            pair.Add("2");
        }

        [Test]
        public void Add_CardWithSameId_CountEq2()
        {
            var pair = new CardPair("1");
            pair.Add("1");

            Assert.AreEqual(2, pair.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Add_CardWithSameIdTwice_Exception()
        {
            var pair = new CardPair("1");
            pair.Add("1");
            pair.Add("1");

            Assert.AreEqual(2, pair.Count);
        }
    }
}