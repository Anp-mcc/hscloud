using System.Collections.Generic;

namespace Entity
{
    public class Card
    {
        public string Id { get; set; }

        public Language? Language { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public CardType Type { get; set; }

        public CardRarity? Rarity { get; set; }

        public Faction? Faction { get; set; }

        public Race? Race { get; set; }

        public PlayerClass? PlayerClass { get; set; }

        public string Text { get; set; }

        public string InPlayText { get; set; }

        public List<string> Mechanics { get; set; }

        public string Flavor { get; set; }

        public string Artist { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public string Durability { get; set; }

        public string Collectible { get; set; }

        public string Elite { get; set; }

        public string HowToGet { get; set; }

        public string HowToGetGold { get; set; }

        public string ImageUrl { get; set; }
    }
}