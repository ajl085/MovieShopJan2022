using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<Favorite> GetFavoriteById(int userId, int movieId);
    }
}
