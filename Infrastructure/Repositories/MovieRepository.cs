using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetOwnedMoviesByUser(int userId)
        {
            // get total owned movies for that user
            var totalMoviesOwnedByUser = await _dbContext.Purchases.Where(p => p.UserId == userId).CountAsync();

            // get the actual movies from MovieGenre and Movie Table
            if (totalMoviesOwnedByUser == 0)
            {
                throw new Exception("User does not own any movies");
            }

            var movies = await _dbContext.Purchases.Where(u => u.UserId == userId).Include(p => p.Movie)
                .OrderBy(p => p.MovieId)
                .Select(p => new Movie
                {
                    Id = p.MovieId,
                    PosterUrl = p.Movie.PosterUrl,
                    Title = p.Movie.Title
                })
                .ToListAsync();

            return movies;
        }

        public override async Task<Movie> GetById(int id)
        {
            // First throws exception if no matches found
            // FirstOrDefault safest
            // Single throws exception 0 or more than 1
            // SingleOrDefault throws exception if more than 1
            // Need to use Include method
            var movieDetails = await _dbContext.Movies.Include(m => m.Genres).ThenInclude(m => m.Genre)
                .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Trailers)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movieDetails;
        }

        public async Task<PagedResultSet<Movie>> GetMoviesByGenres(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // get total movies count for that genre
            var totalMoviesCountByGenre = await _dbContext.MovieGenres.Where(m => m.GenreId == genreId).CountAsync();

            // get the actual movies from MovieGenre and Movie Table
            if (totalMoviesCountByGenre == 0)
            {
                throw new Exception("No Movies Found for that genre");
            }


            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(m => m.Movie)
                .OrderBy(m => m.MovieId)
                .Select(m => new Movie
                {
                    Id = m.MovieId,
                    PosterUrl = m.Movie.PosterUrl,
                    Title = m.Movie.Title
                })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCountByGenre);
            return pagedMovies;
        }

        public async Task<decimal> GetMoviePrice(int movieId)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            return movie.Price.GetValueOrDefault();
        }

        public async Task<IEnumerable<Movie>> GetReviewedMoviesByUser(int userId)
        {
            // get total reviewed movies for that user
            var totalMoviesReviewedByUser = await _dbContext.Reviews.Where(r => r.UserId == userId).CountAsync();

            // get the actual movies from Movie Table
            if (totalMoviesReviewedByUser == 0)
            {
                throw new Exception("User has not made any movie reviews");
            }

            var movies = await _dbContext.Reviews.Where(u => u.UserId == userId).Include(r => r.Movie)
                .OrderBy(r => r.MovieId)
                .Select(r => new Movie
                {
                    Id = r.MovieId,
                    PosterUrl = r.Movie.PosterUrl,
                    Title = r.Movie.Title
                })
                .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetFavoritedMoviesByUser(int userId)
        {
            // get total favorited movies for that user
            var totalMoviesFavoritedByUser = await _dbContext.Favorites.Where(f => f.UserId == userId).CountAsync();

            // get the actual movies from Movie Table
            if (totalMoviesFavoritedByUser == 0)
            {
                throw new Exception("User has not added any movies to favorites");
            }

            var movies = await _dbContext.Favorites.Where(u => u.UserId == userId).Include(f => f.Movie)
                .OrderBy(f => f.MovieId)
                .Select(f => new Movie
                {
                    Id = f.MovieId,
                    PosterUrl = f.Movie.PosterUrl,
                    Title = f.Movie.Title
                })
                .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTop30RatedMovies()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultSet<Movie>> GetAllMovies(int pageSize = 30, int pageNumber = 1)
        {
            // get total movies count
            var totalMoviesCount = await _dbContext.Movies.CountAsync();

            // get the actual movies from Movie Table
            if (totalMoviesCount == 0)
            {
                throw new Exception("No Movies Found");
            }

            var movies = await _dbContext.Movies
                .OrderBy(m => m.Id)
                .Select(m => new Movie
                {
                    Id = m.Id,
                    PosterUrl = m.PosterUrl,
                    Title = m.Title
                })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
            return pagedMovies;
        }
    }
}
