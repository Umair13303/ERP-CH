using OrganisationSetup.Models.DAL;
using SharedUI.Models.SQLParameters;

namespace OrganisationSetup.Interfaces.ApplicationConfiguration
{
    public interface IACCompany
    {

        int? upsertIntoACCompany(PostedData postedData);
        int? duplicateCheck(string operationType,Guid? guID,string description);
    }
}
