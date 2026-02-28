using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganisationSetup.Areas.AccountNfinance.Services;
using OrganisationSetup.Areas.ApplicationConfiguration.Services;
using OrganisationSetup.Areas.Inventory.Services;
using OrganisationSetup.Services;
using SharedUI.Models.Configurations;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Areas.Inventory.Controllers
{
    [Authorize]
    [Area(nameof(SetupRoute.Area.Inventory))]
    public class ICategoryManagementController : Controller
    {
        private readonly IApplicationConfigurationRetriever _acrService;
        private readonly IInventoryRetriever _irService;
        private readonly IInventoryUpsert _iuService;

        public ICategoryManagementController(IApplicationConfigurationRetriever anfrService, IInventoryRetriever irService, IInventoryUpsert iuService)
        {
            _acrService = anfrService;
            _irService = irService;
            _iuService = iuService;
        }

        #region PORTION CONTAIN CODE TO: RENDER VIEW
        public IActionResult CreateUpdate_ICategory_UI(UISetting ui)
        {
            ViewBag.OperationType = ui.OperationType;
            ViewBag.DisplayName = ui.DisplayName;
            return View();
        }
        #endregion
        #region PORTION CONTAIN CODE TO: RETURN DEPENDING DDL
        [HttpGet]
        public async Task<IActionResult> populateBranchListByParam(string operationType)
        {
            var result = await _acrService.populateBranchByParam(operationType, (int?)FilterConditions.acBranch_Operation_ByAllowedBranches,null);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> populateDepartmentListByParam(string operationType, int? locationId)
        {
            var result = await _acrService.populateDepartmentByParam(operationType, (int?)FilterConditions.acDepartment_Operation_ByLocation, locationId);
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
            var result = await _irService.populateCategoryByParam(operationType, (int?)FilterConditions.iCategory_Operation_BySection, sectionId);
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

            var result = await _iuService.updateInsertDataInto_ICategory(postedData);
            return Json(new { IsSuccess = result.IsSuccess, responseCode = result.StatusCode, message = result.Message });
        }
        #endregion
    }
}
