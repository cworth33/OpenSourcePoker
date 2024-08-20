namespace SecureDeck.Models
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        // Constructor to initialize an empty deck
        public Deck()
        {
            Cards = [];
        }

        // Method to initialize the deck with a standard 52-card set
        public void InitializeDeck(List<Card> cards)
        {
            Cards = new List<Card>(cards);
        }

        // Property to get the current count of cards in the deck
        public int Count => Cards.Count;
    }
}
