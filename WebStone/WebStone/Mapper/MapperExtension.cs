using Entity;
using WebStone.Models;

namespace WebStone.Mapper
{
    public static class MapperExtension
    {
        public static DeckViewModel Map(this Deck deck)
        {
            return new DeckViewModel { Name = deck.Name, Hero = deck.Hero };
        }
    }
}