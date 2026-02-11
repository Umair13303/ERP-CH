using IMSClothHouse.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IMSClothHouse.Models.DAL.StoredProcedures
{
    public class StoredProcedureService: IStoredProcedure
    {
        private readonly IConfiguration _configuration;
        public StoredProcedureService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int? upsert_Into_CBranch(SQLParameter postedData)
        {
            string connectionString = _configuration.GetConnectionString("ERPClothHouseConnection");
            int responseCode = 0; using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CBrand_Upsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    cmd.Parameters.AddWithValue("@GuID", postedData.GuID != Guid.Empty ? postedData.GuID : Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@Code", postedData.Code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", postedData.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FocalPerson", postedData.FocalPerson ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Contact", postedData.Contact ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", postedData.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", postedData.Address ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreatedBy", postedData.CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedOn", (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DocType", postedData.DocType);
                    cmd.Parameters.AddWithValue("@DocumentStatus", postedData.DocumentStatus);
                    cmd.Parameters.AddWithValue("@Status", postedData.Status);
                    cmd.Parameters.AddWithValue("@BranchId", postedData.BranchId);
                    cmd.Parameters.AddWithValue("@CompanyId", postedData.CompanyId);
                    cmd.Parameters.AddWithValue("@OperationType", "INSERT_DATA_INTO_DB");

                    SqlParameter outputParam = new SqlParameter("@Response", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    responseCode = (int)outputParam.Value;
                }
            }
            return responseCode;
        }
    }
}