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
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Movie> GetTop30RevenueMovies()
        {
            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30);
            return movies;
        }

        public override Movie GetById(int id)
        {
            // First throws exception if no matches found
            // FirstOrDefault safest
            // Single throws exception 0 or more than 1
            // SingleOrDefault throws exception if more than 1
            // Need to use Include method
            var movieDetails = _dbContext.Movies.Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers).FirstOrDefault(m => m.Id == id);

            return movieDetails;
        }
    }
}
