using Microsoft.AspNetCore.Mvc;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    public class ACUserManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
