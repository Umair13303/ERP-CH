using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;
using SharedUI.Models.Responses;


namespace OrganisationSetup.Areas.Inventory.Services
{
    public interface IInventoryUpsert
    {
        Task<ServiceResult> updateInsertDataInto_ISection(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ICategory(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_ISubCategory(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IBrand(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IProduct(PostedData postedData);
        Task<ServiceResult> updateInsertDataInto_IProductATI(PostedData postedData);

    }
    public class InventoryUpsertService : IInventoryUpsert
    {
        private readonly IOSDataLayer _repo;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInventoryValidation _validationService;
        private readonly ERPOrganisationSetupContext _eRPOSContext;
        public InventoryUpsertService(IOSDataLayer repo, ERPOrganisationSetupContext context, IHttpContextAccessor httpContextAccessor ,IInventoryValidation validationService, ERPOrganisationSetupContext eRPOSC)
        {
            _repo = repo;
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _httpContextAccessor = httpContextAccessor;
            _validationService = validationService;
            _eRPOSContext = eRPOSC;
        }
        public async Task<ServiceResult> updateInsertDataInto_ISection(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isISectionValid(postedData.OperationType, postedData.GuID, postedData.DepartmentId, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_ISection(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
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
        public async Task<ServiceResult> updateInsertDataInto_ICategory(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isICategoryValid(postedData.OperationType, postedData.GuID, postedData.DepartmentId, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_ICategory(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.category,
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
        public async Task<ServiceResult> updateInsertDataInto_ISubCategory(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isISubCategoryValid(postedData.OperationType, postedData.GuID,  postedData.CategoryId, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_ISubCategory(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            postedData.CategoryId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.subCategory,
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
        public async Task<ServiceResult> updateInsertDataInto_IBrand(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isIBrandValid(postedData.OperationType, postedData.GuID, postedData.Description);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_IBrand(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description?.Trim(),
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.brand,
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
        public async Task<ServiceResult> updateInsertDataInto_IProduct(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, postedData.GuID, postedData.Description, postedData.MachineNumber, postedData.SKU);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_IProduct(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.Description?.Trim(),
                                                            postedData.MachineNumber?.Trim(),
                                                            postedData.SKU?.Trim(),
                                                            postedData.AdditionalDetail?.Trim(),
                                                            postedData.AttributeIds?.Trim(),
                                                            postedData.BrandId,
                                                            postedData.IsFavorite,
                                                            postedData.IsSaleTaxExclusive,
                                                            postedData.DepartmentId,
                                                            postedData.SectionId,
                                                            postedData.CategoryId,
                                                            postedData.SubCategoryId,
                                                            postedData.CriticalLimit,
                                                            postedData.SaleUnitId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.product,
                                                            (int?)DocumentStatus.active,
                                                            userInfo.BranchId,
                                                            userInfo.CompanyId,
                                                            con, transaction);
                    await transaction.CommitAsync();
                    var productId = await _eRPOSContext.IProduct.AsNoTracking().Where(x => x.GuID == postedData.GuID).Select(x => (int?)x.Id).FirstOrDefaultAsync();
                    postedData.ProductId = productId.Value;

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
        
        public async Task<ServiceResult> updateInsertDataInto_IProductATI(PostedData postedData)
        {
            var userInfo = TempUser.Fill(_httpContextAccessor);

            if (!userInfo.IsAuthenticated)
                return ServiceResult.failure(Message.serverResponse((int?)Code.Unauthorized), (int)Code.Unauthorized);

            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                postedData.GuID = Guid.NewGuid();
            }
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, postedData.GuID, postedData.Description, postedData.MachineNumber,postedData.SKU);

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    var result = await _repo.UpsertInto_IProductATI(
                                                            postedData.OperationType,
                                                            postedData.GuID,
                                                            postedData.ProductId,
                                                            postedData.InventoryAccountId,
                                                            postedData.SaleRevenueAccountId,
                                                            postedData.CostOfSaleAccountId,
                                                            postedData.ItemTypeId,
                                                            postedData.HSCodeId,
                                                            postedData.SaleTaxTypeId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            DateTime.Now,
                                                            userInfo.UserId,
                                                            (int?)DocumentType.productATI,
                                                            (int?)DocumentStatus.active,
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