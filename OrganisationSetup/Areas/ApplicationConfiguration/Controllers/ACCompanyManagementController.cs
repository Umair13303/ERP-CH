using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Services.ApplicationConfiguration;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class ACCompanyManagementController : Controller
    {
        private readonly ACCompanyService _acCompanyService;
        public ACCompanyManagementController(ACCompanyService acCompanyService)
        {
            _acCompanyService = acCompanyService;
        }

        public IActionResult CreateUpdate_ACCompany_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }

        [HttpPost]
        public IActionResult SaveUpdate(PostedData data)
        {
            try
            {
                var resultId = _acCompanyService.SaveUpdate(data);

                return Json(new { success = true, data = resultId, message = "Record saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}