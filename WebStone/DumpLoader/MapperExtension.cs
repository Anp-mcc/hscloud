using Entity;

namespace DumpLoader
{
    public static class MapperExtension
    {
        public static Card Map(this CardJsonModel card)
        {
            return new Card { Id = card.Id, Cost = card.Cost, Attack = card.Attack, Text = card.Text, Name = card.Name};
        }
    }
}