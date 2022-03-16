using DWPKata.Core.Interfaces;
using DWPKata.Interfaces;
using DWPKata.Models;
using GeoCoordinatePortable;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DWPKata.Services
{
    public class UserBuilderService : IUserBuilderService
    {
        private readonly IUserApiService _userService;
        private readonly IConfiguration _config;

        public UserBuilderService(IUserApiService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public async Task<List<UserModel>> GetUsersInAndAroundLondon()
        {
            // Get the lat and long for the City of London
            var latLondon = _config.GetValue<double>("AppSettings:LondonLatitude");
            var longLondon = _config.GetValue<double>("AppSettings:LondonLongitude");
            var source = new GeoCoordinate(latLondon, longLondon);

            // Get the equivalent distance of 50 miles from London as a measurement in meters
            var fiftyMilesInMeters = _config.GetValue<double>("AppSettings:FiftyMilesInMeters");

            // Get users currently living in London
            var londonUserDtos = await _userService.GetUsersFromLondon();
            var users = londonUserDtos.Adapt<IEnumerable<UserModel>>().ToList();

            // Get all other users and use the GeoCoordinate service to determine which ones live within 50 miles of London
            var allUserDtos = await _userService.GetAllUsers();

            foreach (var userDto in allUserDtos)
            {
                // Get the lat and the long for the user
                var dest = new GeoCoordinate((double)userDto.Latitude, (double)userDto.Longitude);

                // Is the user within a 50 mile radius of London and not already in our list of users living in London
                if (source.GetDistanceTo(dest) <= fiftyMilesInMeters && !users.Any(u => u.Id == userDto.Id))
                    users.Add(userDto.Adapt<UserModel>());
            }

            return users;
        }
    }
}