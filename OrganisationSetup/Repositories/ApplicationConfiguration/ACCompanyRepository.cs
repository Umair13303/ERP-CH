using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Interfaces.ApplicationConfiguration;
using OrganisationSetup.Models.DAL;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using System.Data;

namespace OrganisationSetup.Repositories.ApplicationConfiguration
{
    public class ACCompanyRepository : IACCompany
    {
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ACCompanyRepository(ERPOrganisationSetupContext eRPOSC, IHttpContextAccessor httpContextAccessor)
        {
            _eRPOSContext = eRPOSC;
            _httpContextAccessor = httpContextAccessor;
        }

        public int? duplicateCheck(string operationType, Guid? guID, string description)
        {
            bool IsRecordExist = false;
            int? response = (int?)Code.Unauthorized;

            switch (operationType)
            {
                case nameof(OperationType.INSERT_DATA_INTO_DB):
                    IsRecordExist = _eRPOSContext.ACCompanies
                                .Any(x =>
                                    x.Description == description &&
                                    x.DocumentStatus == (int?)DocumentStatus.active &&
                                    x.Status == true
                                );
                    if (!IsRecordExist)
                        response = (int?)Code.OK;
                    else
                        response = (int?)Code.Unauthorized;
                    break;

                case nameof(OperationType.UPDATE_DATA_INTO_DB):
                    IsRecordExist = _eRPOSContext.ACCompanies
                                .Any(x =>
                                    x.GuID == guID &&
                                    x.Status == true
                                );
                    if (IsRecordExist)
                        response = (int?)Code.OK;
                    else
                        response = (int?)Code.Unauthorized;
                    break;
                default:
                    response = (int?)Code.Unauthorized;
                    break;
            }
            return response;

        }

        public int? upsertIntoACCompany(PostedData postedData)
        {
            string connectionString = _eRPOSContext.Database.GetDbConnection().ConnectionString;
            var userInfo = TempUser.Fill(_httpContextAccessor);
            if (userInfo.IsAuthenticated)
            {
                if(postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
                {
                    postedData.GuID = Guid.NewGuid();
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("ACCompany_Upsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DB_OperationType", (object)postedData.OperationType! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GuID", (object)postedData.GuID! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)postedData.Description! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CountryId", (object)postedData.CountryId! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CityId", (object)postedData.CityId! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", (object)postedData.Contact! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)postedData.Email! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)postedData.Address! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Website", (object)postedData.Website! ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Logo", (object)postedData.Logo! ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@CreatedOn", (object)postedData.CreatedOn! ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreatedBy", userInfo.UserId);

                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedBy", userInfo.UserId);

                    cmd.Parameters.AddWithValue("@DocumentType", (int?)DocumentType.company);
                    cmd.Parameters.AddWithValue("@DocumentStatus", (int?)DocumentStatus.active);
                    cmd.Parameters.AddWithValue("@Status",true );
                    cmd.Parameters.AddWithValue("@BranchId", userInfo.BranchId);
                    cmd.Parameters.AddWithValue("@CompanyId", userInfo.CompanyId);
                    var responseParam = new SqlParameter("@Response", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(responseParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
                }
            }
            else
            {
                return 300;
            }
        }
    }
}