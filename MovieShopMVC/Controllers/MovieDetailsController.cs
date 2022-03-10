using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieDetailsController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Details(int id)
        {
            // Movie Service with Details
            // pass the movie details data to view
            var castDetails = await _movieService.GetMovieDetails(id);
            return View(castDetails);
        }
    }
}
