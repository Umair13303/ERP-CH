using OrganisationSetup.Interfaces.ApplicationConfiguration;
using OrganisationSetup.Repositories.ApplicationConfiguration;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Services.ApplicationConfiguration
{
    public class ACCompanyService
    {
        private readonly IACCompany _repo;

        public ACCompanyService(IACCompany repo)
        {
            _repo = repo;
        }
        public int? SaveUpdate(PostedData data)
        {
            if (string.IsNullOrWhiteSpace(data.Description))
                throw new Exception("Description required");

            return _repo.upsertIntoACCompany(data);
        }
    }
}
