namespace SecureDeck.Models
{
    public class ShuffledDeck
    {
        public required string DeckString { get; set; }                  // The serialized deck as a string
        public required List<ShuffleParticipant> Participants { get; set; } // List of participants (players and operator)
        public required string BitcoinAddress { get; set; }              // The derived Bitcoin address
    }
}
