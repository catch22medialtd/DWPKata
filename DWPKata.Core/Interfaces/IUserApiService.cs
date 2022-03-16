using DWPKata.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DWPKata.Core.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserDto>> GetUsersFromLondon();
        Task<List<UserDto>> GetAllUsers();
    }
}