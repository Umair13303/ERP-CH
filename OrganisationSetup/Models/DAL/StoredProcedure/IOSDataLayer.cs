using Microsoft.Data.SqlClient;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Data;

namespace OrganisationSetup.Models.DAL.StoredProcedure
{
    public interface IOSDataLayer
    {
        Task<int?> UpsertInto_ACCompany(PostedData postedData, TempUser user, SqlConnection con, SqlTransaction trans);
    }
    public class OSDataLayerRepository : IOSDataLayer
    {
        public async Task<int?> UpsertInto_ACCompany(PostedData postedData, TempUser userInfo, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACCompany_Upsert", con, trans);
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
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", userInfo.BranchId);
            cmd.Parameters.AddWithValue("@CompanyId", userInfo.CompanyId);

            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();

            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
    }
}
