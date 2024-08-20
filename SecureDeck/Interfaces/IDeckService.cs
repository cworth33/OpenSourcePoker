using SecureDeck.Models;

namespace SecureDeck.Interfaces
{
    public interface IDeckService
    {
        void ShuffleDeck(List<ShuffleParticipant> shuffleParticipants);
        ShuffledDeck GetFinalPostShuffleDeckState();
    }
}
