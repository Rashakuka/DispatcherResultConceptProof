# DispatcherResult Concept Proof with Pokedex Service

This project demonstrates the use of a custom `DispatcherResult` class to handle successful, informational, and error messages when performing operations. Using the popular Pokémon API, the application fetches data of a specific Pokémon by its name or ID and showcases how the `DispatcherResult` class can be leveraged to inform the user of the outcome.

## Features:
- Unified response handling using `DispatcherResult<T>` class.
- Fetch Pokémon details using the [PokeAPI](https://pokeapi.co/).
- Demonstrates the dependency injection pattern with Microsoft.Extensions.DependencyInjection.
- Simple console interface to interact with the application.

## Getting Started:

1. **Prerequisites:**
   - .NET SDK
   - A code editor like Visual Studio or VS Code.

2. **Installation:**
   - Clone this repository:
     ```bash
     git clone [repository-url]
     ```

   - Navigate to the project directory and restore the required packages:
     ```bash
     dotnet restore
     ```

3. **Usage:**
   - Run the program:
     ```bash
     dotnet run
     ```

   - Follow the on-screen prompts to enter the name or ID of a Pokémon. The program will then fetch the corresponding data and display it. Any error or informational messages will also be presented.

## Architecture:

### Namespaces and their purposes:

1. **DispatcherResultConceptProof.Dispatchers:** 
   - Contains the `DispatcherResult<T>` class, which provides a unified way of returning results from operations, complete with data, success messages, error messages, and informational messages.

2. **DispatcherResultConceptProof.Models:** 
   - Houses the data models used in the application. Currently, it has the `Pokemon` model.

3. **DispatcherResultConceptProof.Services:** 
   - Contains the service interfaces and their implementations. The main service here is the `PokedexService`, which interacts with the PokeAPI to fetch Pokémon details.

4. **DispatcherResultConceptProof:** 
   - The main entry point of the application. It sets up dependency injection, takes user input, calls the Pokedex service, and displays the results.

## Contributions:
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
