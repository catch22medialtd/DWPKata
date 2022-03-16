using DWPKata.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DWPKata.Interfaces
{
    public interface IUserBuilderService
    {
        Task<List<UserModel>> GetUsersInAndAroundLondon();
    }
}