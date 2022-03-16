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
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Review> GetReviewById(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            return review;
        }
    }
}
