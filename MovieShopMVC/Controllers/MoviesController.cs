using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public MoviesController(IMovieService movieService, ICurrentUser currentUser, IUserService userService)
        {
            _movieService = movieService;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<IActionResult> Details(int id)
        {
            // Movie Service with Details
            // pass the movie details data to view
            // check if user is logged in
            // isMoviePurchased(userId, movieId) in repository => true/false
            // call the above method if the user is logged in

            ViewBag.MoviePurchased = false;

            if (_currentUser.IsAuthenticated)
            {
                var purchased = await _userService.IsMoviePurchased(id, _currentUser.UserId);
                ViewBag.MoviePurchased = purchased;
            }
            
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
