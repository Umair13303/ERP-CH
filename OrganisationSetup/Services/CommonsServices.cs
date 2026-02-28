using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Enums;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public interface ICommon
    {
        Task<int?[]?> getDocumentStatusByParam(string operationType);
        Task<List<vOrganisationType>> populateOrganisationTypeByParam();
        Task<List<vCountry>> populateCountryByParam();
        Task<List<vCity>> populateCityByParam(int? countryId);
        Task<List<vRole>> populateRoleByParam();
        Task<List<vAccountType>> populateAccountTypeByParam();
        Task<List<vAccountCatagory>> populateAccountCatagoryByParam(int? accountTypeId);
        Task<List<vFinancialStatement>> populateFinancialStatementByParam();

    }
    public class CommonServices : ICommon
    {
        private readonly ERPOrganisationSetupContext _context;

        public CommonServices( ERPOrganisationSetupContext context)
        {
            _context = context;

        }
        public async Task<int?[]?> getDocumentStatusByParam(string operationType)
        {
            int?[]? documentStatusIds = operationType switch
            {
                nameof(OperationType.INSERT_DATA_INTO_DB) => [(int?)DocumentStatus.active],
                nameof(OperationType.UPDATE_DATA_INTO_DB) => [(int?)DocumentStatus.active, (int?)DocumentStatus.inactive, (int?)DocumentStatus.deleted],
                _ => null
            };
            return await Task.FromResult(documentStatusIds);
        }
        public async Task<List<vOrganisationType>> populateOrganisationTypeByParam()
        {
            var result = await _context.vOrganisationType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCountry>> populateCountryByParam()
        {
            var result = await _context.vCountry.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCity>> populateCityByParam(int? countryId)
        {
            var result = await _context.vCity.AsNoTracking().Where(x=> x.CountryId == countryId).ToListAsync();
            return result;
        }
        public async Task<List<vRole>> populateRoleByParam()
        {
            var result = await _context.vRole.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vAccountType>> populateAccountTypeByParam()
        {
            var result = await _context.vAccountType.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vAccountCatagory>> populateAccountCatagoryByParam(int? accountTypeId)
        {
            var result = await _context.vAccountCatagory.AsNoTracking().Where(x=> x.AccountTypeId == accountTypeId).ToListAsync();
            return result;
        }
        public async Task<List<vFinancialStatement>> populateFinancialStatementByParam()
        {
            var result = await _context.vFinancialStatement.AsNoTracking().ToListAsync();
            return result;
        }
    }
}
