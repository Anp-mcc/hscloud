using Entity;

namespace DumpLoader
{
    public static class MapperExtension
    {
        public static Card Map(this CardJsonModel jsonCard)
        {
            return Map(jsonCard, string.Empty);
        }

        public static Card Map(this CardJsonModel jsonCard, string language)
        {
            var card = new Card();

            card.Id = jsonCard.Id + (string.IsNullOrEmpty(language) ? string.Empty : "_" + language);
            card.Language = language;
            card.Name = jsonCard.Name;
            card.Cost = jsonCard.Cost;
            card.Type = jsonCard.Type;
            card.Rarity = jsonCard.Rarity;
            card.Faction = jsonCard.Faction;
            card.Race = jsonCard.Race;
            card.PlayerClass = jsonCard.PlayerClass;
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
                (string.IsNullOrEmpty(language) ? "enUS" : language).ToLower(), jsonCard.Id);

            return card;
        }
    }
}