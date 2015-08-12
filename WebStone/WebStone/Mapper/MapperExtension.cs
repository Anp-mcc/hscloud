using System.Linq;
using CQS.Query;
using CQS.QueryHandler;
using WebStone.ViewModels;

namespace WebStone.Mapper
{
    public static class MapperExtension
    {
        public static AllDecksViewModel Map(this AllDeckQueryResult allDecks)
        {
            return new AllDecksViewModel {Decks = allDecks.Decks.Select(x => x.Map())};
        }

        public static DeckViewModel Map(this DeckQueryResult deck)
        {
            return new DeckViewModel {Hero = deck.Hero, Id = deck.Id, Name = deck.Name};
        }

        public static CardViewModel Map(this CardQueryResult card)
        {
            return new CardViewModel {Id = card.Id, Name = card.Name, Cost = card.Cost, Attack = card.Attack, Text = card.Text};
        }
    }
}