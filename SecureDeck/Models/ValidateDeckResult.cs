namespace SecureDeck.Models
{
    public class ValidateDeckResult
    {
        public required ShuffledDeck Request { get; set; }
        public required string ResultingBitcoinAddress { get; set; }
        public bool IsValid { get; set; }
        public DateTime ValidatedDate { get; set; }
    }
}
