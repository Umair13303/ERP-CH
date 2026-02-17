using Microsoft.AspNetCore.Mvc;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    public class ACCompanyManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
