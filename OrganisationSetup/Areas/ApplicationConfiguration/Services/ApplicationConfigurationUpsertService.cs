using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.ApplicationConfiguration.Services
{
    public interface IApplicationConfigurationUpsertService
    {
        Task<ServiceResult> updateInsertDataInto_ACCompany(PostedData postedData);
    }
    public class ApplicationConfigurationUpsertService:IApplicationConfigurationUpsertService
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationConfigurationUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResult> updateInsertDataInto_ACCompany(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.Failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if(postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var transaction = con.BeginTransaction();
            try
            {
                var result = await _repo.UpsertInto_ACCompany(postedData, userInfo, con, transaction);
                transaction.Commit();

                return ServiceResult.Success(Message.serverResponse(result), result.Value);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ServiceResult.Failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
            }
        }
    }
}