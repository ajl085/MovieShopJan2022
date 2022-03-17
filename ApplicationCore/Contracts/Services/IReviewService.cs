using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IReviewService
    {
        Task<List<ReviewModel>> GetAllReviewsByMovieId(int id);
    }
}
