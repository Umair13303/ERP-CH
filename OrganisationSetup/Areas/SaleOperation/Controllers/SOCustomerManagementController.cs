using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;

namespace OrganisationSetup.Areas.SaleOperation.Controllers
{

    [Authorize]
    [Area(nameof(SetupRoute.Area.SaleOperation))]
    public class SOCustomerManagementController : Controller
    {
        private readonly ICommon _commonsServices;
        public SOCustomerManagementController( ICommon commonsServices)
        {
            _commonsServices = commonsServices;
        }
        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_SOCustomer_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion
        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populatevCountryListByParam()
        {
            var result = await _commonsServices.populateCountryByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populatevCityListByParam(int? countryId)
        {
            var result = await _commonsServices.populateCityByParam(countryId);
            return Json(result);
        }
        #endregion

    }
}
