using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        // account/register => GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // save the password and account info with salt
            return View();
        }
    }
}
