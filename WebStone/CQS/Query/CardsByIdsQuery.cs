using System.Collections.Generic;
using CQS.Core;

namespace CQS.Query
{
    public class CardsByIdsQuery : IQuery
    {
         public IEnumerable<string> CardIds { get; set; } 
    }
}