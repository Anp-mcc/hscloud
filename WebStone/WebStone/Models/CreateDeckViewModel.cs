using System;
using System.Collections.Generic;
using Entity;

namespace WebStone.Models
{
    public class CreateDeckViewModel
    {
        public IEnumerable<string> HeroTypes { get; private set; }

        public CreateDeckViewModel()
        {
            HeroTypes = Enum.GetNames(typeof (HeroClass));
        }
    }
}