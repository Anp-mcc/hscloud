﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Entity;

namespace DumpLoader
{
    public class CardJsonModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Type { get; set; }

        public string Rarity { get; set; }

        public string Faction { get; set; }

        public string Race { get; set; }

        public CardType PlayerClas { get; set; }

        public string Text { get; set; }

        public string InPlayerText { get; set; }

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
    }
}