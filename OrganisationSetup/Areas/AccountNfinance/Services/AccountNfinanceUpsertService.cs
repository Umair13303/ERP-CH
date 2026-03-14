using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.AccountNfinance.Services
{
    public interface IAccountNfinanceUpsert
    {
        Task<ServiceResult> updateInsertDataInto_AFChartOfAccount(PostedData postedData, bool? isCustomerAutoAccount);



    }
    public class AccountNfinanceUpsertService: IAccountNfinanceUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountNfinanceValidation _validationService;
        private readonly IAccountNfinanceRetriever _retrieverService;
        public AccountNfinanceUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor , IAccountNfinanceValidation validationService, IAccountNfinanceRetriever retrieverService)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _retrieverService = retrieverService;
        }
        public async Task<ServiceResult> updateInsertDataInto_AFChartOfAccount(PostedData postedData, bool? isCustomerAutoAccount)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if(postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isAFChartOfAccountValid(postedData.OperationType,postedData.GuID,postedData.Description);

            if(isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_AFChartOfAccount(
                                                           postedData.OperationType,
                                                           postedData.GuID,
                                                           isCustomerAutoAccount == false ? postedData.Description?.Trim() : postedData.DefaultReceivableAccount?.Trim(),
                                                           postedData.AccountCategoryId,
                                                           postedData.FinancialStatementId,
                                                           DateTime.Now,
                                                           userInfo.UserId,
                                                           DateTime.Now,
                                                           userInfo.UserId,
                                                           (int?)DocumentType.accountChartOfAccount,
                                                           (int?)DocumentStatus.active,
                                                           userInfo.BranchId,
                                                           userInfo.CompanyId,
                                                           con, transaction);

                    await transaction.CommitAsync();

                    var accountInfoByGuID = await _retrieverService.PopulateChartOfAccountInfo(postedData.GuID);
                    return ServiceResult.internalSuccess(Message.serverResponse(result), result.Value, accountInfoByGuID.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }

        }
    }
}