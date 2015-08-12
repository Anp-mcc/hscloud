using System;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace WebStone.Domain
{
    public class CardPair 
    {
        public IEnumerable<string> Ids { get { return Enumerable.Repeat(Id, Count); } }

        public string Id { get; set; }
        public int Count { get; set; }

        public CardPair(string cardId)
        {
            if (cardId == null)
                throw new ArgumentNullException();

            Id = cardId;
            Count = 1;
        }

        public void Add(string cardId)
        {
            if (cardId != Id)
                throw new ArgumentException();

            if(Count == 2)
                throw new ArgumentOutOfRangeException();

            Count += 1;
        }
    }
}