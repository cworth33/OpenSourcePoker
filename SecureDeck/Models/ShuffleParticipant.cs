namespace SecureDeck.Models
{
    public class ShuffleParticipant
    {
        public required string Name { get; set; }  // The name of the player or operator
        public required string Seed { get; set; }  // The seed provided by the player or operator
    }
}
