using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewModel>> GetAllReviewsByMovieId(int id)
        {
            var reviews = await _reviewRepository.GetAllReviewsByMovieId(id);
            var allReviews = new List<ReviewModel>();

            // mapping entities data into models data
            foreach (var review in reviews)
            {
                allReviews.Add(new ReviewModel
                {
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return allReviews;
        }
    }
}
