using System.Collections.Generic;

namespace Entity
{
    public class Deck
    {
        public string Name { get; set; }

        public IEnumerable<Card> Cards { get; set; } 
    }
}
