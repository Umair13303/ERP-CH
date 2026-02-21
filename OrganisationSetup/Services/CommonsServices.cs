using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.ViewModels;

namespace OrganisationSetup.Services
{
    public interface ICommonsServices
    {
        Task<List<vCountry>> populateCountryByParam();
        Task<List<vCity>> populateCityByParam(int? countryId);
    }
    public class CommonServices : ICommonsServices
    {
        private readonly ERPOrganisationSetupContext _context;

        public CommonServices( ERPOrganisationSetupContext context)
        {
            _context = context;

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
    }
}
