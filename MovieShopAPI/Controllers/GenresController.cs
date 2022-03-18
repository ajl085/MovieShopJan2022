using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetGenres();

            if (genres == null)
            {
                return NotFound(new { error = "Genres Not Found" });
            }
            return Ok(genres);
        }
    }
}
