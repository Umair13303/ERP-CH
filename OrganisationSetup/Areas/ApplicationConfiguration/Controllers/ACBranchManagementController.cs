using Microsoft.AspNetCore.Mvc;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    public class ACBranchManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
