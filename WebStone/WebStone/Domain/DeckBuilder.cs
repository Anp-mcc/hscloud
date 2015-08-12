using System;
using System.Collections.Generic;
using System.Linq;
using WebStone.Factories;

namespace WebStone.Domain
{
    public class DeckBuilder<T>
    {
        private readonly IDeckFactory<T> _deckFactory;
        private readonly IList<CardPair> _cards;
        
        public DeckBuilder(IDeckFactory<T> deckFactory)
        {
            _deckFactory = deckFactory;
            _cards = new List<CardPair>();
        }

        public void Push(string cardId)
        {
            if(cardId == null)
                throw new ArgumentNullException();

            var cardPair = _cards.FirstOrDefault(x => x.Id == cardId);

            if(cardPair == null)
                _cards.Add(new CardPair(cardId));
            else
                cardPair.Add(cardId);
        }

        public T Build()
        {
            return _deckFactory.Create(_cards.SelectMany(x => x.Ids));
        }
    }
}