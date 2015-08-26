using CQS.Core;
using Entity;

namespace CQS.Query
{
    public class CardForClassQuery : IQuery
    {
        public int NumberOfCardsOnPage { get; set; }
        public int CurrentPage { get; set; }

        public PlayerClass SelectedHero { get; set; }
    }
}