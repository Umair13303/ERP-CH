using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.ApplicationConfiguration))]
    public class ACCategoryManagementController : Controller
    {
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IApplicationConfigurationUpsert _acuService;

        public ACCategoryManagementController(IApplicationConfigurationRetriever acrService, IApplicationConfigurationUpsert acuService)
        {
            _acrService = acrService;
            _acuService = acuService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ACCategory_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        public IActionResult CreateUpdate_ACSubCategory_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion
        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populateDepartmentListByParam(string operationType)
        {
            var result = await _acrService.populateDepartmentByParam(operationType, (int?)FilterConditions.acDepartment_Operation_ByCompany);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateSectionListByParam(string operationType, int? departmentId)
        {
            var result = await _acrService.populateSectionByParam(operationType, (int?)FilterConditions.acSection_Operation_ByDepartment, departmentId);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateCategoryListByParam(string operationType, int? sectionId)
        {
            var result = await _acrService.populateCategoryByParam(operationType, (int?)FilterConditions.ACCategory_Operation_BySection, sectionId);
            return Json(result);
        }
        #endregion
        #region PORTION CONTAIN CODE TO: ADD/EDIT/DELETE DOCUMENT
        [HttpPost]
        public async Task<IActionResult> createUpdateCategory([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_ACCategory(postedData);
            return Json(new { IsSuccess = result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        [HttpPost]
        public async Task<IActionResult> createUpdateSubCategory([FromBody] PostedData postedData)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (!ModelState.IsValid) return View(postedData);

            var result = await _acuService.updateInsertDataInto_ACSubCategory(postedData);
            return Json(new { IsSuccess = result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
