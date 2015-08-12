using System;
using System.Collections.Generic;
using System.Linq;
using CQS.Core;
using CQS.Mappers;
using CQS.Query;
using CQS.QueryResult;
using DataAccess;
using Entity;

namespace CQS.QueryHandler
{
    public class AllCardsQueryHandler : IQueryHandler<AllCardsQuery, AllCardsQueryResult>
    {
        private readonly IDatabaseCore _core;

        public AllCardsQueryHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public AllCardsQueryResult Retrieve(AllCardsQuery query)
        {
            if(query == null)
                throw new ArgumentException();
            
            using (var session = _core.OpenSession())
            {
                var pagingInfo = new PagingInfo
                {
                    CurrentPage = query.CurrentPage,
                    ItemsPerPage = query.NumberOfCardsOnPage,
                    TotalItems = session.Query<Card>().Count()
                };

                var queryResult = new AllCardsQueryResult { PagingInfo = pagingInfo};

                var cards = session.Query<Card>().Skip(query.NumberOfCardsOnPage * (query.CurrentPage - 1)).Take(query.NumberOfCardsOnPage);
                queryResult.CardViewModels = cards.ToList().Select(x => x.Map());

                return queryResult;
            }
        }
    }

    public class AllCardsQueryResult : IQueryResult
    {
        public PagingInfo PagingInfo { get; set; }

        public IEnumerable<CardQueryResult> CardViewModels { get; set; }
    }
}