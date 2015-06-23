using System.Collections.Generic;

namespace WebStone.Models
{
    public class DisplayDeckModel
    {
        public string Name { get; set; }
        public IEnumerable<Card> DisplayDeck { get; set; }
    }

    public class Card
    {
        public string Name { get; set; }
    }
}