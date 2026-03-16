using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrganisationSetup.Models.DAL;
using OrganisationSetup.Models.DAL.StoredProcedure;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.Responses;
using SharedUI.Models.SQLParameters;
using System.Diagnostics;


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

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? sectionGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                sectionGuID = Guid.NewGuid();
            }
            else
            {
                sectionGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isISectionValid(postedData.OperationType, sectionGuID,postedData.DepartmentId, postedData.Description);
            #endregion
            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ACUser
                    var ISection = await _repo.UpsertInto_ISection(
                                                            postedData.OperationType,
                                                            sectionGuID,
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
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    int? sectionResponse = ISection.Value;
                    switch (sectionResponse)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            break;
                        default:
                            await transaction.RollbackAsync();
                            break;
                    }
                    #endregion
                    return ServiceResult.success(Message.serverResponse(sectionResponse), sectionResponse.Value);
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

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? categoryGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                categoryGuID = Guid.NewGuid();
            }
            else
            {
                categoryGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isICategoryValid(postedData.OperationType, categoryGuID, postedData.DepartmentId, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ACUser
                    var ICategory = await _repo.UpsertInto_ICategory(
                                                            postedData.OperationType,
                                                            categoryGuID,
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
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    int? categoryResponse = ICategory.Value;
                    switch (categoryResponse)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            break;
                        default:
                            await transaction.RollbackAsync();
                            break;
                    }
                    #endregion

                    return ServiceResult.success(Message.serverResponse(categoryResponse), categoryResponse.Value);
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

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? subCategoryGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                subCategoryGuID = Guid.NewGuid();
            }
            else
            {
                subCategoryGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isISubCategoryValid(postedData.OperationType, subCategoryGuID,postedData.CategoryId, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.ISubCategory
                    var ISubCategory = await _repo.UpsertInto_ISubCategory(
                                                            postedData.OperationType,
                                                            subCategoryGuID,
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
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    int? subCategoryResponse = ISubCategory.Value;
                    switch (subCategoryResponse)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            break;
                        default:
                            await transaction.RollbackAsync();
                            break;
                    }
                    #endregion
                    return ServiceResult.success(Message.serverResponse(subCategoryResponse), subCategoryResponse.Value);
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

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? brandGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                brandGuID = Guid.NewGuid();
            }
            else
            {
                brandGuID = postedData.GuID;
            }
            bool? isOperationPermitted = await _validationService.isIBrandValid(postedData.OperationType, brandGuID, postedData.Description);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.IBrand
                    var IBrand = await _repo.UpsertInto_IBrand(
                                                            postedData.OperationType,
                                                            brandGuID,
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
                    #endregion

                    #region PORTION FOR :: HANLDE TRANSACTION
                    int? brandResponse = IBrand.Value;
                    switch (brandResponse)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            await transaction.CommitAsync();
                            break;
                        default:
                            await transaction.RollbackAsync();
                            break;
                    }
                    #endregion

                    return ServiceResult.success(Message.serverResponse(brandResponse), brandResponse.Value);
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

            #region PORTION FOR :: DOCUMENT SETTING ON BASIS OF OperationType
            Guid? productGuID = Guid.Empty;
            Guid? productATIGuID = Guid.Empty;
            if (postedData.OperationType == nameof(OperationType.INSERT_DATA_INTO_DB))
            {
                productGuID = Guid.NewGuid();
                productATIGuID = Guid.NewGuid();
            }
            else
            {
                productGuID = postedData.GuID;
                productATIGuID = postedData.ProductATIGuID;
            }
            bool? isOperationPermitted = await _validationService.isIProductValid(postedData.OperationType, productGuID, postedData.Description, postedData.MachineNumber, postedData.SKU);
            #endregion

            if (isOperationPermitted == true)
            {
                using var con = new SqlConnection(_connectionString);
                await con.OpenAsync();
                using var transaction = con.BeginTransaction();
                try
                {
                    #region PORTION FOR :: UPSERT INTO dbo.IProduct
                    var IProduct = await _repo.UpsertInto_IProduct(
                                                            postedData.OperationType,
                                                            productGuID,
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
                    #endregion

                    int? productResponse = IProduct.response;
                    switch (productResponse)
                    {
                        case (int)Code.Created:
                        case (int)Code.Accepted:
                            #region PORTION FOR :: UPSERT INTO dbo.IProductATI
                            var IProductATI = await _repo.UpsertInto_IProductATI(
                                                            postedData.OperationType,
                                                            productATIGuID,
                                                            IProduct.insertedId!.Value,
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
                            #endregion

                            #region PORTION FOR :: HANLDE TRANSACTION
                            int? productATIResponse = IProductATI.Value;
                            switch (IProductATI)
                            {
                                case (int)Code.Created:
                                case (int)Code.Accepted:
                                    await transaction.CommitAsync();
                                    break;
                                default:
                                    await transaction.RollbackAsync();
                                    return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                            }
                            #endregion
                            break;
                        default:
                            await transaction.RollbackAsync();
                            return ServiceResult.failure(Message.serverResponse((int?)Code.BadRequest), (int)Code.BadRequest);
                    }
                    return ServiceResult.success(Message.serverResponse(productResponse), productResponse!.Value);
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