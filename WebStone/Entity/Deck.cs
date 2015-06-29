using System.Collections.Generic;

namespace Entity
{
    public class Deck
    {
        public Deck()
        {
            Cards = new List<Card>();
        }

        public string Name { get; set; }

        public IEnumerable<Card> Cards { get; set; } 

        public HeroClass Hero { get; set; }
    }
}
