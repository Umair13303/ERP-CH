using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Interfaces;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.AOSUser.Controllers
{
    [Authorize]
    [Area(nameof(OSRoute.Area.AOSUser))]
    [Route(nameof(OSRoute.Area.AOSUser) + "/" + nameof(OSRoute.Api.APIMenu))]
    public class APIMenuController : Controller
    {
        private readonly IMenuService _iMenuService;

        public APIMenuController(IMenuService iMenuService)
        {
            _iMenuService = iMenuService;
        }
        [HttpGet("getMenuForUserRole")]
        public async Task<IActionResult> getMenuForUserRole(int userId, string roleId)
        {
            var menu = await _iMenuService.getMenuForUserRole();
            return Ok(menu);
        }
    }
}
