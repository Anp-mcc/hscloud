using System.Collections.Generic;

namespace WebStone.Models
{
    public class DeckViewModel
    {
        public string Name { get; set; }
        public IEnumerable<string> CardNames { get; set; }
    }
}