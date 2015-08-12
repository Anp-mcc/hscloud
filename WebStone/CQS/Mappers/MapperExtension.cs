using CQS.Query;
using Entity;

namespace CQS.Mappers
{
    public static class MapperExtension
    {
        public static DeckQueryResult Map(this Deck deck)
        {
            return new DeckQueryResult { Id = deck.Id, Name = deck.Name, Hero = deck.Hero };
        }

        public static CardQueryResult Map(this Card card)
        {
            return new CardQueryResult { Id = card.Id, Attack = card.Attack, Name = card.Name, Text = card.Text };
        }
    }
}