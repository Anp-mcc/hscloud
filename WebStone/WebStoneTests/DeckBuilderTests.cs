using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using WebStone.Domain;
using WebStone.Factories;
using WebStone.ViewModels;

namespace WebStoneTests
{
    [TestFixture]
    public class DeckBuilderTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Push_Null()
        {
            var deckFactory = new Mock<IDeckFactory<DeckViewModel>>();
            var target = new DeckBuilder<DeckViewModel>(deckFactory.Object);
            target.Push(null);
        }

        [Test]
        public void Push_Card_ResultedDeckContainsOneCard()
        {
            var deckFactory = new Mock<IDeckFactory<DeckViewModel>>();
            var target = new DeckBuilder<DeckViewModel>(deckFactory.Object); 
            
            target.Push("");
            target.Build();

            deckFactory.Verify(x => x.Create(It.Is<IEnumerable<string>>(z => z.Count() == 1)));
        }

        [Test]
        public void Push_ThreeCardsWithSameId_ResultedDeckContainsOneCard()
        {

            var deckFactory = new Mock<IDeckFactory<DeckViewModel>>();
            var target = new DeckBuilder<DeckViewModel>(deckFactory.Object); var cardId = "1";

            target.Push(cardId);
            target.Push(cardId);

            target.Push("2");

            var deck = target.Build();

            deckFactory.Verify(x => x.Create(It.Is<IEnumerable<string>>(z => z.Count() == 3)));
        }
    }
}