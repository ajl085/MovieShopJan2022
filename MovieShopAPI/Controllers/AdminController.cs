using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    // Role-based authorization
    public class AdminController : ControllerBase
    {
        // can only be accessed with roles of admin or Super Admin

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie()
        {
            return Ok();
        }
    }
}
