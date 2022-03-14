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

        public UserController(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUser.UserId;
            //var movies = await _movieService.GetOwnedMoviesByUser(userId);
            return View();
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
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel model)
        {
            var userId = _currentUser.UserId;
            await _userService.PurchaseMovie(model, userId);
            return LocalRedirect("~/");
            return View();
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
