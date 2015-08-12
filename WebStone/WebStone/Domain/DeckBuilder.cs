using System;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace WebStone.Domain
{
    public class DeckBuilder
    {
        private readonly IList<CardPair> _cards;
        
        public DeckBuilder()
        {
            _cards = new List<CardPair>();
        }

        public void Push(Card card)
        {
            if(card == null)
                throw new ArgumentNullException();

            var cardPair = _cards.FirstOrDefault(x => x.Id == card.Id);

            if(cardPair == null)
                _cards.Add(new CardPair(card));
            else
                cardPair.Add(card);
        }

        public Deck Build()
        {
            return new Deck {CardsIds = _cards.SelectMany(x => x.Ids)};
        }
    }
}