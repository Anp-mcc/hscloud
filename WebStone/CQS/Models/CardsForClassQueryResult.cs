using System.Collections.Generic;
using CQS.Core;

namespace CQS.Models
{
    public class CardsForClassQueryResult : IQueryResult
    {
        public IEnumerable<CardViewModel> Cards { get; set; }
    }
}
