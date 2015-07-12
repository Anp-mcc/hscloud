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

        public IEnumerable<string> CardsIds { get; set; }

        public HeroClass Hero { get; set; }
    }
}
