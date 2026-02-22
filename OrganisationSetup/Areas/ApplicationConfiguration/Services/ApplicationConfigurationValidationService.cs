using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.ApplicationConfiguration.Services
{
    public interface IApplicationConfigurationValidationService
    {
        Task<bool> isACCompanyValid(string? operationType, Guid? guID, string? description);
        Task<bool> isACBranchValid(string? operationType, Guid? guID, string? description);

    }

    public class ApplicationConfigurationValidationService : IApplicationConfigurationValidationService
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public ApplicationConfigurationValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }

        public async Task<bool> isACCompanyValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ACCompanies
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ACCompanies.AnyAsync(x => x.GuID == guID);

                    return exists;

                default:
                    return false;
            }
        }

        public async Task<bool> isACBranchValid(string? operationType, Guid? guID, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ACBranches
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower());

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ACBranches.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }
    }
}