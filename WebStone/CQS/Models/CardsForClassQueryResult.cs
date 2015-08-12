using System.Collections.Generic;
using CQS.Core;
using CQS.Query;

namespace CQS.Models
{
    public class CardsForClassQueryResult : IQueryResult
    {
        public IEnumerable<CardQueryResult> Cards { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
