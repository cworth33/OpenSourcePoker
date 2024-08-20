using DeckValidationServcie.Services;
using DeckValidationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SecureDeck.Interfaces;
using SecureDeck.Models;
using SecureDeck.Services;

namespace DeckValidationServcie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckValidationController : Controller
    {
        private readonly IDeckService _deckService;
        private readonly IDeckValidator _validator;

        public DeckValidationController(IDeckService deckService, IDeckValidator validator)
        {
            _deckService = deckService;
            _validator = validator;
        }

        [HttpPost("GetDeck")]
        public IActionResult GetDeck([FromBody] List<ShuffleParticipant> shuffleParticipants)
        {
            try
            {
                _deckService.ShuffleDeck(shuffleParticipants);
                return Ok(_deckService.GetFinalPostShuffleDeckState());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("ValidateDeck")]
        public IActionResult ValidateDeck([FromBody] ShuffledDeck request)
        {
            try
            {
                return Ok(_validator.ValidateDeck(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
