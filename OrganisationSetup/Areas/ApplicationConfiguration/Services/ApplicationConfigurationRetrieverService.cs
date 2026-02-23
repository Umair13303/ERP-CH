using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;
using System;
using System.Linq;

namespace OrganisationSetup.Areas.ApplicationConfiguration.Services
{
    public interface IApplicationConfigurationRetriever
    {
        Task<List<ACCompany>> populateCompanyByParam(string operationType, int? filterConditionId);
        Task<List<ACBranch>> populateBranchByParam(string operationType, int? filterConditionId, int? companyId);


    }
    public class ApplicationConfigurationRetrieverService : IApplicationConfigurationRetriever
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationConfigurationRetrieverService(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<List<ACCompany>> populateCompanyByParam(string operationType, int? filterConditionId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ACCompany>();
            }

            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            if (documentStatusIds == null) return new List<ACCompany>();
            List<ACCompany> companyRecord = new List<ACCompany>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.acCompany_ApplicationConfiguration):
                    return await _eRPOSContext.ACCompanies.AsNoTracking()
                        .Where(x =>
                        x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACCompany
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ACCompany>();
            }
        }
        public async Task<List<ACBranch>> populateBranchByParam(string operationType, int? filterConditionId, int? companyId)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (!userInfo.IsAuthenticated)
            {
                return new List<ACBranch>();
            }

            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            if (documentStatusIds == null) return new List<ACBranch>();
            List<ACBranch> BranchRecord = new List<ACBranch>();
            switch (filterConditionId)
            {
                case ((int?)FilterConditions.acBranch_ApplicationConfiguration):
                    return await _eRPOSContext.ACBranches.AsNoTracking()
                        .Where(x =>
                        x.CompanyId == companyId
                        && x.Status == true
                        && documentStatusIds.Contains(x.DocumentStatus)).Select(x => new ACBranch
                        {
                            Id = x.Id,
                            GuID = x.GuID,
                            Description = x.Description
                        }).ToListAsync();
                default:
                    return new List<ACBranch>();
            }
        }
    }
}
