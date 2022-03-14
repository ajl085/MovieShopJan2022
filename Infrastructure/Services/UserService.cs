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
        private readonly IMovieService _movieService;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task FavoriteExists(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task GetAllFavoritesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var movies = await _movieService.GetOwnedMoviesByUser(id);
            var movieCards = new List<MovieCardModel>();

            // mapping entities data into models data
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public Task GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            // check whether user has already purchased the movie
            // go to purchase repository and get purchase record from purchase table by userId and movieId

            var dbPurchase = await _purchaseRepository.GetPurchaseById(userId, purchaseRequest.movieId);

            if (dbPurchase != null)
            {
                return true;
            }
            return false;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // check whether user has already purchased the movie
            // go to purchase repository and get purchase record from purchase table by userId and movieId

            var dbPurchase = await _purchaseRepository.GetPurchaseById(userId, purchaseRequest.movieId);

            if (dbPurchase == null)
            {
                // continue with purchase

                var newPurchase = new Purchase
                {
                    UserId = userId,
                    TotalPrice = purchaseRequest.Price,
                    PurchaseDateTime = DateTime.Now,
                    MovieId = purchaseRequest.movieId
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
