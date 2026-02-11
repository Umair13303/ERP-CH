using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.OSAUser.Controllers
{
    [Area(nameof(OSRoute.Area.AOSUser))]
    [Authorize]
    public class OSUDashboardController : Controller
    {

        public IActionResult OSUDashboardDefault() => View();
    }
}
