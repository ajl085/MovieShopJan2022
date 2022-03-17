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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetGenres()
        {
            var genres = await _genreRepository.GetGenres();
            var genreList = new List<GenreModel>();

            // mapping entities data into models data
            foreach (var genre in genres)
            {
                genreList.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return genreList;
        }
    }
}
