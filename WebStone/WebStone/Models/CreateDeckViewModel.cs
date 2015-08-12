using System;
using System.Collections.Generic;
using Entity;
using WebStone.ViewModels;

namespace WebStone.Models
{
    public class CreateDeckViewModel
    {
        public IEnumerable<string> HeroTypes { get; private set; }
        public IEnumerable<CardViewModel> Cards { get; set; }

        public DeckViewModel Deck { get; set; } 
        
        public CreateDeckViewModel()
        {
            HeroTypes = Enum.GetNames(typeof (HeroClass));
        }
    }
}