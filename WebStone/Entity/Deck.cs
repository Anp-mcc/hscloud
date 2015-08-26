using System;
using System.Collections.Generic;

namespace Entity
{
    public class Deck
    {
        public Deck()
        {
            CardsIds = new List<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }
        public PlayerClass Class { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public int? ViewsCount { get; set; }
        public int? CommentsCount { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }

        public IEnumerable<string> CardsIds { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string AuthorUrl { get; set; }
        public string DeckUrl { get; set; }
    }
}
