namespace SecureDeck.Models
{
    public class Card(RankType rank, SuitType suit)
    {

        // Properties for Rank and Suit
        public RankType Rank { get; private set; } = rank;
        public SuitType Suit { get; private set; } = suit;

        // Method to display the card as a string (e.g., "Ace of Spades")
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        // Method to return the shorthand notation of the card (e.g., Kc for King of Clubs)
        public string ToShortString()
        {
            string rank = Rank switch
            {
                RankType.Two => "2",
                RankType.Three => "3",
                RankType.Four => "4",
                RankType.Five => "5",
                RankType.Six => "6",
                RankType.Seven => "7",
                RankType.Eight => "8",
                RankType.Nine => "9",
                RankType.Ten => "T",
                RankType.Jack => "J",
                RankType.Queen => "Q",
                RankType.King => "K",
                RankType.Ace => "A",
                _ => ""
            };

            string suit = Suit switch
            {
                SuitType.Hearts => "h",
                SuitType.Diamonds => "d",
                SuitType.Clubs => "c",
                SuitType.Spades => "s",
                _ => ""
            };

            return $"{rank}{suit}";
        }
    }
}
