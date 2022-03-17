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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetById(id);

            var movieDetails = new MovieDetailsModel
            {
                Id = movie.Id, Price = movie.Price, Budget = movie.Budget, Overview = movie.Overview, Revenue = movie.Revenue, Tagline = movie.Tagline, Title = movie.Title, ImdbUrl = movie.ImdbUrl, RunTime = movie.RunTime, BackdropUrl = movie.BackdropUrl, PosterUrl = movie.PosterUrl, ReleaseDate = movie.ReleaseDate, TmdbUrl = movie.TmdbUrl
            };

            movieDetails.Reviews = new List<ReviewModel>();
            foreach (var review in movie.Reviews)
            {
                movieDetails.Reviews.Add(new ReviewModel
                {   
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            var numRatingCount = 0;
            var ratingTotal = 0.0m;

            foreach (var rating in movie.Reviews)
            {
                ratingTotal += rating.Rating;
                numRatingCount++;
            }
            movieDetails.Rating = Decimal.Round(ratingTotal / numRatingCount, 1);

            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = genre.GenreId, Name = genre.Genre.Name
                });
            }

            movieDetails.Casts = new List<CastModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastModel
                {
                    Id = cast.CastId,
                    Character = cast.Character,
                    Name = cast.Cast.Name,
                    Gender = cast.Cast.Gender,
                    ProfilePath = cast.Cast.ProfilePath,
                    TmdbUrl = cast.Cast.TmdbUrl
                });
            }

            movieDetails.Trailers = new List<TrailerModel>();
            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }

            return movieDetails;
        }

        public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
        {
            var movies = await _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardModel>();

            // mapping entities data into models data
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel 
                {
                    Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetMoviesByGenres(genreId, pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            movieCards.AddRange(pagedMovies.Data.Select(m => new MovieCardModel
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            }));

            return new PagedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);
        }

        public async Task<List<MovieCardModel>> GetOwnedMoviesByUser(int userId)
        {
            var movies = await _movieRepository.GetOwnedMoviesByUser(userId);
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

        public async Task<PurchaseRequestModel> GetPurchaseRequestModel(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetMoviePrice(int movieId)
        {
            return await _movieRepository.GetMoviePrice(movieId);
        }

        public async Task<List<MovieCardModel>> GetReviewedMoviesByUser(int userId)
        {
            var movies = await _movieRepository.GetReviewedMoviesByUser(userId);
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

        public async Task<List<MovieCardModel>> GetFavoritedMoviesByUser(int userId)
        {
            var movies = await _movieRepository.GetFavoritedMoviesByUser(userId);
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

        public async Task<List<MovieCardModel>> GetTop30RatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();
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

        public async Task<PagedResultSet<MovieCardModel>> GetAllMoviesByPagination(int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieRepository.GetAllMovies(pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            movieCards.AddRange(pagedMovies.Data.Select(m => new MovieCardModel
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            }));

            return new PagedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);
        }
    }
}
