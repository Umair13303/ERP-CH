using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class ACCompanyManagementController : Controller
    {
        private readonly IApplicationConfigurationUpsertService _acuService;
        private readonly ICommonsServices _commonsServices;
        public ACCompanyManagementController(IApplicationConfigurationUpsertService acCompanyService,ICommonsServices commonsServices)
        {
            _commonsServices = commonsServices;
            _acuService = acCompanyService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ACCompany_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion

        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL\
        [HttpGet]
        public async Task<IActionResult> populateCountryListByParam()
        {
            var result = await _commonsServices.populateCountryByParam();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateCityListByParam(int? countryId)
        {
            var result = await _commonsServices.populateCityByParam(countryId);
            return Json(result);
        }
        #endregion

        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateCompany([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);
            
            var result = await _acuService.updateInsertDataInto_ACCompany(postedData);
            return Json(new {IsSuccess= result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}