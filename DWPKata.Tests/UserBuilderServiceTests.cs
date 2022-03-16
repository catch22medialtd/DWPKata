using DWPKata.Core.Dtos;
using DWPKata.Core.Interfaces;
using DWPKata.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DWPKata.Tests
{
    [TestClass]
    public class UserBuilderServiceTests
    {

        [TestMethod]
        public async Task Assert_GetUsersInAndAroundLondon_ReturnsExpectedUsers()
        {
            //Arrange
            var svc = new Mock<IUserApiService>();

            var inMemoryConfig = new Dictionary<string, string> {
                {"AppSettings:LondonLatitude", "51.509865"},
                {"AppSettings:LondonLongitude", "-0.118092"},
                {"AppSettings:FiftyMilesInMeters", "80467.2"}
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfig)
                .Build();

            var londonUsers = GetLondonUsers();
            var allUsers = GetAllUsers();

            svc.Setup(s => s.GetUsersFromLondon())
                .ReturnsAsync(londonUsers);

            svc.Setup(s => s.GetAllUsers())
                .ReturnsAsync(allUsers);

            var builder = new UserBuilderService(svc.Object, config);

            //Act
            var users = await builder
                .GetUsersInAndAroundLondon();

            //Assert
            // London residents have only been added once
            foreach (var user in londonUsers)
                Assert.IsTrue(users.Count(u => u.Id == user.Id) == 1);

            // Total 3
            Assert.IsTrue(users.Count == 3);
        }

        private static List<UserDto> GetLondonUsers()
        {
            return new List<UserDto>()
            {
                new UserDto
                {
                    Id = 1,
                    FirstName = "Bill",
                    LastName = "Smith",
                    Email = "b.smith@test123.co.uk",
                    IPAddress = "192.168.2.1",
                    Latitude = 51.4991m,
                    Longitude = 0.1644m
                },
                new UserDto
                {
                    Id = 2,
                    FirstName = "Ken",
                    LastName = "Smith",
                    Email = "k.smith@test123.co.uk",
                    IPAddress = "192.168.2.2",
                    Latitude = 51.4991m,
                    Longitude = 0.1644m
                }
            };
        }

        private static List<UserDto> GetAllUsers()
        {
            return new List<UserDto>()
            {
                // Lives in London
                new UserDto
                {
                    Id = 1,
                    FirstName = "Bill",
                    LastName = "Smith",
                    Email = "b.smith@test123.co.uk",
                    IPAddress = "192.168.2.1",
                    Latitude = 51.4991m,
                    Longitude = 0.1644m
                },
                // Lives in London
                new UserDto
                {
                    Id = 2,
                    FirstName = "Ken",
                    LastName = "Smith",
                    Email = "k.smith@test123.co.uk",
                    IPAddress = "192.168.2.2",
                    Latitude = 51.4991m,
                    Longitude = 0.1644m
                },
                // Leeds coordinates so not in London
                new UserDto
                {
                    Id = 3,
                    FirstName = "Wayne",
                    LastName = "Smith",
                    Email = "w.smith@test123.co.uk",
                    IPAddress = "192.168.2.3",
                    Latitude = 53.8008m,
                    Longitude = 1.5491m
                },
                // Harrow coordinates so in London
                new UserDto
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "j.smith@test123.co.uk",
                    IPAddress = "192.168.2.4",
                    Latitude = 51.5725m,
                    Longitude = 0.3334m
                },
                // Rochdale coordinates so not in London
                new UserDto
                {
                    Id = 3,
                    FirstName = "Ian",
                    LastName = "Smith",
                    Email = "i.smith@test123.co.uk",
                    IPAddress = "192.168.2.5",
                    Latitude = 53.6097m,
                    Longitude = 2.1561m
                }
            };
        }
    }
}