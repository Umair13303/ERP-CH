using OrganisationSetup.Models.DAL;
using SharedUI.Models.Enums;
using Microsoft.EntityFrameworkCore; // Required for async methods

namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryValidation
    {
        Task<bool> isICategoryValid(string? operationType, Guid? guID, int? locationId,int?sectionId, string? description);
    }

    public class InventoryValidationService : IInventoryValidation
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;

        public InventoryValidationService(ERPOrganisationSetupContext eRPOSC)
        {
            _eRPOSContext = eRPOSC;
        }

        public async Task<bool> isICategoryValid(string? operationType, Guid? guID, int? locationId, int? sectionId, string? description)
        {
            if (string.IsNullOrEmpty(operationType)) return false;
            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    return !await _eRPOSContext.ICategory
                        .AnyAsync(x => x.Description!.Trim().ToLower() == description!.Trim().ToLower() && x.SectionId == sectionId);

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    bool exists = await _eRPOSContext.ICategory.AnyAsync(x => x.GuID == guID);
                    return exists;
                default:
                    return false;
            }
        }

    }
}