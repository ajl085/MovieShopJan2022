﻿using ApplicationCore.Contracts.Repositories;
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

        public async Task<CastDetailsModel> GetCastDetails(int id)
        {
            var cast = await _castRepository.GetById(id);

            var castDetails = new CastDetailsModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath
            };

            castDetails.Movies = new List<MovieCardModel>();
            foreach (var movieCard in cast.Movies)
            {
                castDetails.Movies.Add(new MovieCardModel
                {
                    Id = movieCard.MovieId,
                    Title = movieCard.Movie.Title
                });
            }

            return castDetails;
        }
    }
}
