using System.Collections.Generic;

namespace WebStone.Models
{
    public class DeckViewModel
    {
        public string Name { get; set; }
        public string Hero { get; set; }
        public IEnumerable<string> CardNames { get; set; }

        PagingInfo PagingInfo { get; set; }
    }
}