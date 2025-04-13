using RickAndMorty.Core.Entities;
using RickAndMorty.Infrastructure.Data;

namespace RickAndMorty.IntegrationTests
{
    internal class Seeding
    {
        public static void IntializeTestDb(RickAndMortyDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if data already exists to avoid duplicate seeding
            if (!context.Characters.Any() && !context.Locations.Any())
            {
                // Seed Locations
                var locations = new List<Location>
            {
                new Location { Id=1, Name = "Earth (C-137)", Type="Planet", Dimension="Dimension C-137",Url = "https://rickandmortyapi.com/api/location/3" },
                new Location { Id=2, Name = "Citadel of Ricks",Type="Rocks",Dimension="Dimension C-137", Url = "https://rickandmortyapi.com/api/location/3" },
                new Location { Id=3, Name = "Pluto", Type="Planet",Dimension="Dimension C-137", Url = "https://rickandmortyapi.com/api/location/3" }
            };
                context.Locations.AddRange(locations);

                // Seed Characters
                var characters = new List<Character>
            {
                new Character
                {
                    Id=1,
                    Name = "Rick Sanchez",
                    Gender = "Male",
                    Status = "Alive",
                    Species = "Human",
                    Type = "Scientist",
                    Origin = locations[0],
                    Location = locations[0],
                    Image = "https://rickandmortyapi.com/api/character/avatar/1.jpeg"
                },
                new Character
                {
                    Id=2,
                    Name = "Morty Smith",
                    Gender = "Male",
                    Status = "Alive",
                    Species = "Human",
                    Type = "Sidekick",
                    Origin = locations[1],
                    Location = locations[1],
                    Image = "https://rickandmortyapi.com/api/character/avatar/1.jpeg"
                },
                new Character
                {
                    Id=3,
                    Name = "Evil Morty",
                    Gender = "Male",
                    Status = "Unknown",
                    Species = "Human",
                    Type = "Villain",
                    Origin = locations[2],
                    Location = locations[2],
                     Image = "https://rickandmortyapi.com/api/character/avatar/1.jpeg"
                }
            };
                context.Characters.AddRange(characters);

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}