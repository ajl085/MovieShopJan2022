using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopAPI.Services;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        // only authorized user can access
        // we need to tell API app to look for JWT instead of cookie

        private ICurrentUser _currentUser;
        private IUserService _userService;
        private IMovieService _movieService;

        public UserController(IUserService userService, ICurrentUser currentUser, IMovieService movieService)
        {
            _userService = userService;
            _currentUser = currentUser;
            _movieService = movieService;
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUser.UserId;
            var movies = await _userService.GetAllPurchasesForUser(userId);

            if (movies == null)
            {
                return NotFound(new { error = "Movies Not Found" });
            }
            return Ok(movies);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie(int movieId)
        {
            var userId = _currentUser.UserId;
            var moviePrice = await _movieService.GetMoviePrice(movieId);
            var purchaseRequest = new PurchaseRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = moviePrice,
                PurchaseDateTime = DateTime.UtcNow
            };

            await _userService.PurchaseMovie(purchaseRequest, userId);

            var movies = await _userService.GetAllPurchasesForUser(userId);

            if (movies == null)
            {
                return NotFound(new { error = "Movies Not Found" });
            }
            return Ok(movies);
        }
    }
}
