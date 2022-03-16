using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Favorite> GetFavoriteById(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
            return favorite;
        }
    }
}
