using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    // AttributeRouting
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // in REST pattern we don't specify the http verbs in the url
        // http://movieshop.com/api/movies/2 => JSON DATA
        // http://movieshop.com/movies/details/2 => VIEW

        private IMovieService _movieService;
        private IReviewService _reviewService;

        public MoviesController(IMovieService movieService, IReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        //[Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllMoviesByPagination()
        {
            var movies = await _movieService.GetAllMoviesByPagination(30, 1);

            if (movies == null)
            {
                return NotFound(new { error = "Movies Not Found" });
            }
            return Ok(movies);
        }

        // api/movies/3
        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            // return the data/json format
            // HTTP Status code, 200 OK
            // 404

            if (movie == null)
            {
                return NotFound( new { error = $"Movie Not Found for id: {id}" });
            }
            return Ok(movie);

            // in old .net for JSON serialization we used JSON.NET library => very very popular
            // System.Text => 
        }

        [Route("top-grossing")]
        [HttpGet]
        public async Task<IActionResult> GetTop30GrossingMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();

            if (movies == null)
            {
                return NotFound(new { error = "Movies Not Found" });
            }
            return Ok(movies);
        }

        [Route("genre/{genreId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMoviesByGenrePagination(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenrePagination(genreId, 30, 1);

            if (movies == null)
            {
                return NotFound(new { error = $"Movies Not Found for id: {genreId}" });
            }
            return Ok(movies);
        }

        [Route("{id:int}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetAllReviewsByMovieId(int id)
        {
            var reviews = await _reviewService.GetAllReviewsByMovieId(id);

            if (reviews == null)
            {
                return NotFound(new { error = $"Reviews Not Found for id: {id}" });
            }
            return Ok(reviews);
        }

        [Route("top-rated")]
        [HttpGet]
        public async Task<IActionResult> GetTop30RatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();

            if (movies == null)
            {
                return NotFound(new { error = "Movies Not Found" });
            }
            return Ok(movies);

        }
    }
}
