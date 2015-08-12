using System.Collections.Generic;
using CQS.Core;
using CQS.Query;

namespace CQS.QueryResult
{
    public class CardsByIdsQueryResult : IQueryResult
    {
         public IEnumerable<CardQueryResult> Cards { get; set; } 
    }
}