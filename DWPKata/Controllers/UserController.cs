using DWPKata.Interfaces;
using DWPKata.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DWPKata.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBuilderService _userBuilderService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBuilderService userBuilderService, ILogger<UserController> logger)
        {
            _userBuilderService = userBuilderService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUsersInAndAroundLondon")]
        [SwaggerOperation(
            Summary = "Gets all users living in London and users whose geo coordinates are within 50 miles of London"
            )]
        public async Task<List<UserModel>> GetUsersInAndAroundLondon()
        {
            return await _userBuilderService
                .GetUsersInAndAroundLondon();
        }
    }
}