using System.Linq;
using Entity;
using WebStone.Converter;
using WebStone.Models;

namespace WebStone.Mapper
{
    public class DeckMapper
    {
        private readonly CardIdToNameConverter _cardIdToNameConverter;

        public DeckMapper(CardIdToNameConverter cardIdToNameConverter)
        {
            _cardIdToNameConverter = cardIdToNameConverter;
        }

        public DeckViewModel Map(Deck deck)
        {
            return new DeckViewModel { Name = deck.Name, CardNames = deck.Cards.Select(x => _cardIdToNameConverter.Convert(x.Id))};
        }
    }
}