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
        Task<ServiceResult> updateInsertDataInto_ACBranch(PostedData postedData);

    }
    public class ApplicationConfigurationUpsertService:IApplicationConfigurationUpsertService
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationConfigurationValidationService _validationService;
        public ApplicationConfigurationUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,IApplicationConfigurationValidationService validationService)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
        }
        public async Task<ServiceResult> updateInsertDataInto_ACCompany(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if(postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isACCompanyValid(postedData.OperationType,postedData.GuID,postedData.Description);

            if(isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_ACCompany(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description!.Trim(),
                                                            postedData.CountryId,
                                                            postedData.CityId,
                                                            postedData.Contact!.Trim(),
                                                            postedData.Email!.Trim(),
                                                            postedData.Address!.Trim(),
                                                            postedData.Website!.Trim(),
                                                            null,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.company,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    transaction.Commit();

                    return ServiceResult.success(Message.serverResponse(result), result.Value);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ServiceResult.failure(Message.serverResponse((int?)Code.InternalServerError), (int)Code.InternalServerError);
                }
            }
            else
            {
                return ServiceResult.failure(Message.serverResponse((int?)Code.Conflict), (int)Code.Conflict);
            }

        }
        public async Task<ServiceResult> updateInsertDataInto_ACBranch(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isACCompanyValid(postedData.OperationType, postedData.GuID, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_ACBranch(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description!.Trim(),
                                                            postedData.OrganisationTypeId,
                                                            postedData.CountryId,
                                                            postedData.CityId,
                                                            postedData.Contact!.Trim(),
                                                            postedData.Email!.Trim(),
                                                            postedData.Address!.Trim(),
                                                            postedData.NTNNumber!.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.branch,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    transaction.Commit();

                    return ServiceResult.success(Message.serverResponse(result), result.Value);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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