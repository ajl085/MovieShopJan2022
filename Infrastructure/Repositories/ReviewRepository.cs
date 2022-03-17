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

        public async Task<IEnumerable<Review>> GetAllReviewsByMovieId(int movieId)
        {
            // get total reviews for that movie
            var totalReviewsForMovie = await _dbContext.Reviews.Where(r => r.MovieId == movieId).CountAsync();

            // get the actual reviews from Review Table
            if (totalReviewsForMovie == 0)
            {
                throw new Exception("No reviews found for that movie");
            }

            var reviews = await _dbContext.Reviews.Where(r => r.MovieId == movieId).Include(m => m.Movie)
                .OrderBy(m => m.MovieId)
                .Select(r => new Review
                {
                    MovieId = movieId,
                    UserId = r.UserId,
                    Rating = r.Rating,
                    ReviewText = r.ReviewText
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetReviewById(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            return review;
        }
    }
}
