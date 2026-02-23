using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public interface ICommon
    {
        Task<List<vCountry>> populateOrganisationTypeByParam();
        Task<List<vCountry>> populateCountryByParam();
        Task<List<vCity>> populateCityByParam(int? countryId);
        Task<List<vCity>> populateRoleByParam();


    }
    public class CommonServices : ICommon
    {
        private readonly ERPOrganisationSetupContext _context;

        public CommonServices( ERPOrganisationSetupContext context)
        {
            _context = context;

        }
        public async Task<List<vCountry>> populateOrganisationTypeByParam()
        {
            var result = await _context.vCountries.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCountry>> populateCountryByParam()
        {
            var result = await _context.vCountries.AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<List<vCity>> populateCityByParam(int? countryId)
        {
            var result = await _context.vCities.AsNoTracking().Where(x=> x.CountryId == countryId).ToListAsync();
            return result;
        }
        public async Task<List<vCity>> populateRoleByParam()
        {
            var result = await _context.vCities.AsNoTracking().Take(10).ToListAsync();
            return result;
        }

    }
}
