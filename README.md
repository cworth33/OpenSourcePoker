# OpenSourcePoker

OpenSourcePoker is an open-source poker server project designed to be highly customizable and scalable. This project aims to provide a solid foundation for developers to build upon, including features like deck shuffling, hand evaluation, game management, and more.

## Features

- **Deck Shuffling and Validation**: Secure and transparent deck shuffling using cryptographic techniques.
- **Game Management**: Basic game management logic for handling multiple tables and hands.
- **Scalability**: Designed with scalability in mind, allowing for horizontal scaling across multiple servers.
- **Extensible**: Easily extendable to support new game types, betting structures, and more.

## Roadmap

Here are some of the future enhancements planned for this project:

1. **Hand Evaluation**: Implement advanced hand evaluation algorithms for various poker games.
2. **Pot Management**: Manage complex pot scenarios, including split pots and side pots.
3. **Tournament Management**: Add support for managing poker tournaments, including player registration, table balancing, and payouts.
4. **Game Type Expansion**: Support for additional poker variants like Omaha, Stud, and more.
5. **Pub/Sub Architecture**: Implement a publish/subscribe architecture for better scalability and real-time updates.
6. **Security Enhancements**: Strengthen security measures to prevent cheating and ensure fair play.
7. **Client Integration**: Develop clients for web, mobile, and desktop platforms with different models, including subscription-based and Bitcoin-based systems.
8. **Blockchain Integration**: Explore the use of blockchain technology to decentralize the poker server, providing a more transparent and tamper-proof environment.

## Getting Started with Development

### Prerequisites

Before you begin, ensure you have the following tools installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Cloning the Repository

To get started with development, clone the repository:

```bash
git clone https://github.com/YourUsername/OpenSourcePoker.git
cd OpenSourcePoker
```

### Building the Project

Build the project using the following command:

```bash
dotnet build
```

### Running Tests

To ensure everything is set up correctly, run the tests:

```bash
dotnet test
```

### Starting the Server

Start the server locally to begin development:

```bash
dotnet run --project DeckValidationService
```

## Using the Deck Validation Controller

The `DeckValidationController` provides endpoints to create and validate shuffled decks. Below is an example of how to interact with this controller.

### Get a Shuffled Deck

To get a shuffled deck, send a POST request to the `/api/DeckValidation/GetDeck` endpoint with a list of participants:

```json
POST https://opensourcepoker.azurewebsites.net/api/DeckValidation/GetDeck
[
    {
      "Name": "Operator",
      "Seed": "randomOperatorSeed"
    },
    {
      "Name": "Player1",
      "Seed": "seed1"
    },
    {
      "Name": "Player2",
      "Seed": "seed2"
    }
]
```

### Validate a Shuffled Deck

To validate a shuffled deck, send a POST request to the `/api/DeckValidation/ValidateDeck` endpoint with the deck information:

```json
POST https://opensourcepoker.azurewebsites.net/api/DeckValidation/ValidateDeck
{
  "deckString": "Td Kc Jd 5d 9h 4h Kd 9s Jc Th 8s 2h 4c 3s Js 6c Kh Qc 7d Qh 6h Ts 5c 8h 5s Ks 9d 8d 2s Ad Ac 3c 7s 4d 2c Qs 3h 4s Tc 5h 2d 6d 7h 7c 9c Ah Jh Qd As 3d 8c 6s",
  "participants": [
    {
      "name": "Operator",
      "seed": "randomOperatorSeed"
    },
    {
      "name": "Player1",
      "seed": "seed1"
    },
    {
      "name": "Player2",
      "seed": "seed2"
    }
  ],
  "bitcoinAddress": "13CpqgrUDpLTVnPdz5JfpWocJpTen1fCSU"
}
```

The server will respond with a validation result indicating whether the deck is valid.

## Contributing

We welcome contributions from the community! Please read our [contributing guidelines](CONTRIBUTING.md) before submitting a pull request.

## Donations

If you find this project useful and would like to support its development, consider making a donation:

- **Bitcoin Address**: `1CWorthYvuYoq3teEBbkviKxP2ZcoYeee`

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions or feedback, feel free to reach out via email at [cworth33@gmail.com](mailto:cworth33@gmail.com).
