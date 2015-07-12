using Entity;

namespace CQS.Models
{
    public class DeckViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public HeroClass Hero { get; set; }
    }
}