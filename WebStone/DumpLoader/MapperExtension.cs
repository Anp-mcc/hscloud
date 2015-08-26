using Entity;
using System;

namespace DumpLoader
{
    public static class MapperExtension
    {
        public static Card Map(this CardJsonModel jsonCard)
        {
            return Map(jsonCard, string.Empty);
        }

        public static Card Map(this CardJsonModel jsonCard, string languageString)
        {
            bool languageSpecified = !string.IsNullOrEmpty(languageString);

            var card = new Card();

            card.Id = jsonCard.Id + (!languageSpecified ? string.Empty : "_" + languageString);
            
            if (languageSpecified)
            {
                Language language;
                if (!Enum.TryParse<Language>(languageString, out language))
                    throw new Exception("Cannot recognize language " + languageString);
                card.Language = language;
            }

            CardType cardType;
            if (!Enum.TryParse<CardType>(jsonCard.Type.Replace(" ", string.Empty), out cardType))
                throw new Exception("Cannot recognize card type " + jsonCard.Type);
            card.Type = cardType;

            if (!string.IsNullOrEmpty(jsonCard.Rarity))
            {
                CardRarity cardRarity;
                if (!Enum.TryParse<CardRarity>(jsonCard.Rarity, out cardRarity))
                    throw new Exception("Cannot recognize card rarity " + jsonCard.Rarity);
                card.Rarity = cardRarity;
            }

            if (!string.IsNullOrEmpty(jsonCard.Faction))
            {
                Faction faction;
                if (!Enum.TryParse<Faction>(jsonCard.Faction, out faction))
                    throw new Exception("Cannot recognize faction " + jsonCard.Faction);
                card.Faction = faction;
            }

            if (!string.IsNullOrEmpty(jsonCard.Race))
            {
                Race race;
                if (!Enum.TryParse<Race>(jsonCard.Race, out race))
                    throw new Exception("Cannot recognize race " + jsonCard.Race);
                card.Race = race;
            }

            if (!string.IsNullOrEmpty(jsonCard.PlayerClass))
            {
                PlayerClass playerClass;
                if (!Enum.TryParse<PlayerClass>(jsonCard.PlayerClass, out playerClass))
                    throw new Exception("Cannot recognize player class " + jsonCard.PlayerClass);
                card.PlayerClass = playerClass;
            }

            card.Name = jsonCard.Name;
            card.Cost = jsonCard.Cost;
            card.Text = jsonCard.Text;
            card.InPlayText = jsonCard.InPlayText;
            card.Mechanics = jsonCard.Mechanics;
            card.Flavor = jsonCard.Flavor;
            card.Artist = jsonCard.Artist;
            card.Attack = jsonCard.Attack;
            card.Health = jsonCard.Health;
            card.Durability = jsonCard.Durability;
            card.Collectible = jsonCard.Collectible;
            card.Elite = jsonCard.Elite;
            card.HowToGet = jsonCard.HowToGet;
            card.HowToGetGold = jsonCard.HowToGetGold;

            card.ImageUrl = string.Format("http://wow.zamimg.com/images/hearthstone/cards/{0}/original/{1}.png",
                (!languageSpecified ? "enUS" : languageString).ToLower(), jsonCard.Id);

            return card;
        }
    }
}