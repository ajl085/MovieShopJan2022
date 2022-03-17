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
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var genres = await _dbContext.Genres
            .Select(g => new Genre
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToListAsync(); ;

            return genres;
        }
    }
}
