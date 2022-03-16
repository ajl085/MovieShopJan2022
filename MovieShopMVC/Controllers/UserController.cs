using Infrastructure.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public UserController(ICurrentUser currentUser, IUserService userService, IMovieService movieService)
        {
            _currentUser = currentUser;
            _userService = userService;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUser.UserId;
            var ownedMovies = await _userService.GetAllPurchasesForUser(userId);
            return View(ownedMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(int movieId)
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
            return RedirectToAction("Purchases");
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReviewMovie()
        {
            return View();
        }
    }
}
