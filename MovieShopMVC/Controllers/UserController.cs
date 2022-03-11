using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // first thing is whether user is logged in
            // get the user id
            // send the user id to database to get all the movies user purchased
            // Cookie based authentication
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
        public async Task<IActionResult> BuyMovie()
        {
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
