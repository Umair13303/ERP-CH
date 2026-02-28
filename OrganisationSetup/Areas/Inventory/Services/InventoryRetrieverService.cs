using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Services;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;

namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryRetriever
    {
        Task<List<ICategory>> populateCategoryByParam(string operationType, int? filterConditionId, int? sectionId);

    }
    public class InventoryRetrieverService : IInventoryRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommon _commonsServices;


        public InventoryRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor, ICommon commonsServices)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
            _commonsServices = commonsServices;

        }
        public async Task<List<ICategory>> populateCategoryByParam(string operationType, int? filterConditionId, int? sectionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ICategory>();
            }

            int?[]? documentStatusIds = await _commonsServices.getDocumentStatusByParam(operationType);
            if (documentStatusIds == null) return new List<ICategory>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.iCategory_Operation_BySection):
                    return await _eRPOSContext.ICategory.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == userInfo.CompanyId
                        && x.SectionId ==sectionId 
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ICategory
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ICategory>();
            }
        }

    }
}
