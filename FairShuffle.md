## Provably Fair Deck Shuffling

The deck shuffling process in OpenSourcePoker is based on a **provably fair** system, which ensures that the shuffle is verifiable and transparent to all participants. This is achieved using **cryptographic techniques** to generate and combine randomness from multiple sources, including the players and the operator.

### How the Shuffling Works:

1. **Player Seeds**: Before the shuffle begins, each player and the operator generate their own random seed (a string of characters) which will be used to influence the shuffle. These seeds are independent and unknown to others, ensuring fairness.

2. **Seed Combination**: The seeds from all participants are combined using a cryptographic hashing function, such as **SHA-256**, to produce a final combined seed. This combined seed serves as the entropy source for shuffling the deck.

3. **Deck Initialization**: The deck is initialized with a standard set of 52 cards, representing all possible card combinations in a typical poker game.

4. **Shuffling with Seed**: The combined seed is used to shuffle the deck. A deterministic random number generator (RNG) seeded with the combined hash ensures that the shuffle is influenced by the inputs of all participants, making it impossible for any single participant to predict or control the outcome.

5. **Bitcoin Address Representation**: After the shuffle, the deck is serialized into a string (e.g., "As Ks Qd ..."). This deck string, along with the combined seed, is used to generate a **Bitcoin private key** using the **NBitcoin** library. The corresponding **Bitcoin public address** serves as a tamper-proof representation of the deck's final state.

### How Validation Works:

Once a deck has been shuffled and played, it can be validated to ensure that the shuffle was conducted fairly. Here’s how the validation process works:

1. **Recomputing the Combined Seed**: The validation process starts by taking the seeds from all participants (players and operator) and combining them using the same cryptographic hash function that was used during shuffling.

2. **Recomputing the Deck Hash**: The deck string from the shuffle, combined with the recomputed seed, is hashed to produce a new cryptographic hash. This ensures that the deck hasn’t been altered after it was shuffled.

3. **Bitcoin Address Matching**: The recomputed deck hash and seed are used to regenerate the Bitcoin private key, and from there, the public Bitcoin address is derived. If the generated Bitcoin address matches the one that was created when the deck was shuffled, it proves that the shuffle was fair and no tampering occurred.

### Why It’s Provably Fair:

- **Transparency**: All participants can verify that their seed was included in the shuffle process.
- **Security**: Cryptographic techniques ensure that no single party can influence the shuffle in their favor.
- **Verifiability**: After the hand is completed, the deck and shuffle process can be validated using the Bitcoin address to confirm that no tampering occurred.

This process guarantees fairness and transparency, providing peace of mind for all participants.
