using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;
using OrganisationSetup.Areas.AccountNfinance.Services;


namespace OrganisationSetup.Areas.SaleOperation.Services
{
    public interface ISaleOperationUpsert
    {
        Task<ServiceResult> updateInsertDataInto_SOCustomer(PostedData postedData);

    }
    public class SaleOperationUpsertService : ISaleOperationUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISaleOperationValidation _validationService;
        private readonly IAccountNfinanceUpsert _anfUService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        public SaleOperationUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,ISaleOperationValidation validationService, ERPOrganisationSetupContext eRPOSC, IAccountNfinanceUpsert anfUService)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _anfUService = anfUService;
            _eRPOSContext = eRPOSC;
        }

        public async Task<ServiceResult> updateInsertDataInto_SOCustomer(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isOSCustomerValid(postedData.OperationType, postedData.GuID, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PRESERVE DATA & GENERATE DEFAULT CUSTOMER ACCOUNTS
                    string? customerName = postedData.Description;
                    string accountReceivableDescription = customerName + "--" + "Account Receivable" + Guid.NewGuid().ToString("N").Substring(0, 8);
                    int? accountTypeId = 1;
                    int? accountCatagoryId = 20;

                    postedData.Description = accountReceivableDescription;
                    postedData.AccountTypeId = accountTypeId;
                    postedData.AccountCategoryId = accountCatagoryId;

                    var accountReceivableGeneration = await _anfUService.updateInsertDataInto_AFChartOfAccount(postedData);

                    postedData.ReceivableAccountId = accountReceivableGeneration.DocumentNumber;
                    postedData.Description = customerName;

                    #endregion


                    var result = await _repo.UpsertInto_SOCustomer(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description!.Trim(),
                                                            postedData.Contact!.Trim(),
                                                            postedData.Email!.Trim(),
                                                            postedData.CNICNumber!.Trim(),
                                                            postedData.Address!.Trim(),
                                                            postedData.AdditionalDetail!.Trim(),
                                                            postedData.ReceivableAccountId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.section,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    await transaction.CommitAsync();

                    return ServiceResult.success(Message.serverResponse(result), result.Value);
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