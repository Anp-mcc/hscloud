using System.Collections.Generic;
using Entity;

namespace WebStone.ViewModels
{
    public class DeckViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public PlayerClass Hero { get; set; }

        public IEnumerable<CardViewModel> Cards { get; set; }
    }
}