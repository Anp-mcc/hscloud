namespace Entity
{
    public class Card
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public int Attack { get; set; }

        public int Cost { get; set; }
        public CardType Class { get; set; }
    }
}