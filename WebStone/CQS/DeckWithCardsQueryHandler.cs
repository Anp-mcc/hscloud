﻿using System;
using System.Collections.Generic;
using System.Linq;
using CQS.Core;
using CQS.Query;
using DataAccess;
using Entity;

namespace CQS
{
    public class DeckWithCardsQueryHandler : IQueryHandler<DeckWithCardsQuery, DeckWithCardsQueryResult>
    {  
        private readonly IDatabaseCore _core;

        public DeckWithCardsQueryHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public DeckWithCardsQueryResult Retrieve(DeckWithCardsQuery query)
        {
            if(query == null)
                throw new ArgumentException();

            using (var session = _core.OpenSession())
            {
                var deck = session.Load<Deck>(query.Id);
                var cardNames = new List<string>();

                foreach (var cardId in deck.CardsIds)
                {
                    var card = session.Load<Card>(cardId);
                    cardNames.Add(card.Name);
                }

                return new DeckWithCardsQueryResult() { CardNames = cardNames };
            }
        }
    }

    public class DeckWithCardsQueryResult : IQueryResult
    {
        public IEnumerable<string> CardNames { get; set; } 
    }
}