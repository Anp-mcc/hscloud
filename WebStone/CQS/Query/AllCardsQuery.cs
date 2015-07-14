using CQS.Core;

namespace CQS.Query
{
    public class AllCardsQuery : IQuery
    {
        public AllCardsQuery()
        {
            NumberOfCardsOnPage = 20;
        }

        public int NumberOfCardsOnPage { get; set; }
        public int CurrentPage { get; set; }
    }
}