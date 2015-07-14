using CQS.Models;
using Entity;

namespace CQS.Mappers
{
    public static class MapperExtension
    {
        public static DeckViewModel Map(this Deck deck)
        {
            return new DeckViewModel { Id = deck.Id, Name = deck.Name, Hero = deck.Hero };
        }

        public static CardViewModel Map(this Card card)
        {
            return new CardViewModel {Id = card.Id, Attack = card.Attack, Name = card.Name, Text = card.Text};
        }
    }
}