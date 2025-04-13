# RickAndMorty Application

## Features  
- **Save and Show Alive Characters**: Fetch and display alive characters from the [Rick and Morty API](https://rickandmortyapi.com/api/character/), and save them to a database.  
- **Add New Characters**: Users can add custom characters to the database.  
- **Filter Characters by Planet**: Filter characters based on their planet of origin.  
- **Unit and Integration Tests**: Both unit tests and integration tests are implemented using xUnit to ensure the application’s functionality and stability.  
- **API Retries with Polly**: The application uses **Polly**, a resilience library, to automatically retry failed API requests, ensuring more robust communication with external services.  
- **Caching with MemoryCache**: The application uses **MemoryCache** to cache frequently accessed data and improve performance.

## Technologies Used  
- **IDE**: Visual Studio 2022  
- **Framework**: .NET 8  
- **Frontend**: Blazor WebAssembly  
- **Database**: SQL Server  
- **Architecture**: Clean Architecture  
- **Pattern**: CQRS (Command Query Responsibility Segregation)  
- **Testing**: xUnit  
- **Logging**: Serilog  
- **Polly**: A resilience library for handling retries on API requests  
- **MemoryCache**: For caching frequently accessed data

## Setup Instructions  

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/isurumahesh/RickAndMorty.git
   cd RickAndMortyApp
   ```

2. **Run Migrations**  
   Use Entity Framework Core to create the database:
   ```bash
   dotnet ef database update
   ```

3. **Run the Application**  
   Set both the API and UI projects as startup projects in Visual Studio and run them.

## Deployment  
The project uses GitHub Actions for CI/CD, enabling automated builds, tests, and deployments. The application has been deployed to a free Azure web plan, so sometimes it may take a little extra time to boot up. You can access it via the following URLs:  
- **API**: [https://rickandmortyapi-e5gxd0g2hvdmb5d9.westeurope-01.azurewebsites.net/swagger/index.html](https://rickandmortyapi-e5gxd0g2hvdmb5d9.westeurope-01.azurewebsites.net/swagger/index.html)  
- **UI**: [https://rickandmortyui-hrgec4gafffbhjby.westeurope-01.azurewebsites.net](https://rickandmortyui-hrgec4gafffbhjby.westeurope-01.azurewebsites.net)

## Acknowledgments  
- [Rick and Morty API](https://rickandmortyapi.com/) for providing free access to data.  
- The creators and developers behind the *Rick and Morty* series.
