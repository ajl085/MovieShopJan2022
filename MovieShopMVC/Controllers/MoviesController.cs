using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUser _currentUser;

        public MoviesController(IMovieService movieService, ICurrentUser currentUser)
        {
            _movieService = movieService;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> Details(int id)
        {
            // Movie Service with Details
            // pass the movie details data to view
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            return View("PagedMovies", pagedMovies);
        }
    }
}
