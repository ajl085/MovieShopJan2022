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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public CastDetailsModel GetCastDetails(int id)
        {
            var cast = _castRepository.GetById(id);

            var castDetails = new CastDetailsModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath
            };

            castDetails.MovieCards = new List<MovieCardModel>();
            foreach (var movieCard in cast.MovieCasts)
            {
                castDetails.MovieCards.Add(new MovieCardModel
                {
                    Id = movieCard.MovieId,
                    Title = movieCard.Movie.Title
                });
            }

            return castDetails;
        }
    }
}
