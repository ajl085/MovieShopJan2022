using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        // have all the business logic methods relating to Movies
        Task<List<MovieCardModel>> GetTop30GrossingMovies();
        Task<List<MovieCardModel>> GetTop30RatedMovies();
        Task<MovieDetailsModel> GetMovieDetails(int id);
        Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1);
        Task<PagedResultSet<MovieCardModel>> GetAllMoviesByPagination(int pageSize = 30, int pageNumber = 1);
        Task<List<MovieCardModel>> GetOwnedMoviesByUser(int userId);
        Task<List<MovieCardModel>> GetReviewedMoviesByUser(int userId);
        Task<List<MovieCardModel>> GetFavoritedMoviesByUser(int userId);
        Task<PurchaseRequestModel> GetPurchaseRequestModel(int movieId);
        Task<decimal> GetMoviePrice(int movieId);
    }
}
