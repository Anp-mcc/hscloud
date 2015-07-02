using Entity;

namespace WebStone.Models
{
    public class DeckViewModel
    {
        public string Name { get; set; }
        public HeroClass Hero { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}