using SecureDeck.Models;

namespace DeckValidationService.Interfaces
{
    public interface IDeckValidator
    {
        ValidateDeckResult ValidateDeck(ShuffledDeck request);
    }
}
