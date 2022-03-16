using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieService _movieService;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IMovieService movieService, IMovieRepository movieRepository, IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _movieService = movieService;
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            // check whether user has already added the movie to favorites
            // go to favorite repository and get favorite record from favorite table by userId and movieId

            var dbFavorite = await _favoriteRepository.GetFavoriteById(favoriteRequest.UserId, favoriteRequest.MovieId);

            if (dbFavorite == null)
            {
                // continue with new favorite

                var newFavorite = new Favorite
                {
                    MovieId = favoriteRequest.MovieId,
                    UserId = favoriteRequest.UserId
                };

                // save favorite to Favorite Table
                await _favoriteRepository.Add(newFavorite);
            }
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            // check whether user has already made a review on this movie
            // go to review repository and get review record from review table by userId and movieId

            var dbReview = await _reviewRepository.GetReviewById(reviewRequest.UserId, reviewRequest.MovieId);

            if (dbReview == null)
            {
                // continue with new review

                var newReview = new Review
                {
                    MovieId = reviewRequest.MovieId,
                    UserId = reviewRequest.UserId,
                    Rating = reviewRequest.Rating,
                    ReviewText = reviewRequest.ReviewText
                };

                // save review to Review Table
                await _reviewRepository.Add(newReview);
            }
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task FavoriteExists(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieCardModel>> GetAllFavoritesForUser(int id)
        {
            var movies = await _movieService.GetFavoritedMoviesByUser(id);

            return movies;
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var movies = await _movieService.GetOwnedMoviesByUser(id);
            
            return movies;
        }

        public async Task<List<MovieCardModel>> GetAllReviewsByUser(int id)
        {
            var movies = await _movieService.GetReviewedMoviesByUser(id);

            return movies;
        }

        public async Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId)
        {
            var dbPurchase = await _purchaseRepository.GetPurchaseById(userId, movieId);

            var purchaseDetails = new PurchaseDetailsModel
            {
                Id = dbPurchase.Id,
                UserId = userId,
                PurchaseNumber = dbPurchase.PurchaseNumber,
                TotalPrice = dbPurchase.TotalPrice,
                PurchaseDateTime = dbPurchase.PurchaseDateTime,
                MovieId = movieId
            };

            return purchaseDetails;

        }

        public async Task<bool> IsMoviePurchased(int movieId, int userId)
        {
            // check whether user has already purchased the movie
            // go to purchase repository and get purchase record from purchase table by userId and movieId

            var dbPurchase = await _purchaseRepository.GetPurchaseById(userId, movieId);

            if (dbPurchase == null)
            {
                return false;
            }
            return true;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // check whether user has already purchased the movie
            // go to purchase repository and get purchase record from purchase table by userId and movieId

            var dbPurchase = await _purchaseRepository.GetPurchaseById(userId, purchaseRequest.MovieId);

            if (dbPurchase == null)
            {
                // continue with purchase

                var newPurchase = new Purchase
                {
                    UserId = userId,
                    PurchaseNumber = purchaseRequest.PurchaseNumber,
                    TotalPrice = purchaseRequest.TotalPrice,
                    PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                    MovieId = purchaseRequest.MovieId
                };

                // save purchase to Purchase Table
                await _purchaseRepository.Add(newPurchase);
            }
        }

        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}
