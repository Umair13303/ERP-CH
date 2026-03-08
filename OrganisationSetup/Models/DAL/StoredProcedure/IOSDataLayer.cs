using Azure;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SharedUI.Models.Contexts;
using SharedUI.Models.Enums;
using SharedUI.Models.SQLParameters;
using System.Data;
using System.Net.NetworkInformation;

namespace OrganisationSetup.Models.DAL.StoredProcedure
{
    public interface IOSDataLayer
    {
        Task<int?> UpsertInto_ACCompany(string? operationType, Guid? guId, string? description, int? countryId, int? cityId, string? contact, string? email, string? address, string? website, string? logo, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy,int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ACBranch(string? operationType, Guid? guId, string? description, int? organisationTypeId, int? countryId, int? cityId, string? contact, string? email, string? address, string? ntnNumber, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ACUser(string? operationType, Guid? guId, string? description, string? password, string? contact, string? email, int? employeeId, int? roleId, string? allowedBranchIds, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus,  int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_CSDepartment(string? operationType, Guid? guId,string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ISection(string? operationType, Guid? guId, string? description, int? departmentId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ICategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_ISubCategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, int? categoryId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_IBrand(string? operationType, Guid? guId,string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_IProduct(string? operationType, Guid? guId, string? description, string? machineNumber, string? sku, string? additionalDetail, string? attributeIds, int? brandId, bool? isFavorite, bool? isSaleTaxExclusive, int? departmentId, int? sectionId, int? categoryId, int? subCategoryId, decimal? criticalLimit, int? saleUnitId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans);
        Task<int?> UpsertInto_IProductATI(string? operationType, Guid? guId,  int? productId, int? inventoryAccountId, int? saleRevenueAccountId, int? costOfSaleAccountId, int? itemTypeId, int? hsCodeId, int? saleTaxTypeId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, SqlConnection con, SqlTransaction trans);

    }
    public class OSDataLayerRepository : IOSDataLayer
    {
        public async Task<int?> UpsertInto_ACCompany(string? operationType, Guid? guId, string? description, int? countryId, int? cityId, string? contact, string? email, string? address, string? website, string? logo, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACCompany_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CountryId", (object)countryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CityId", (object)cityId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)address! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Website", (object)website! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Logo", (object)logo! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);
            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ACBranch(string? operationType, Guid? guId, string? description, int? organisationTypeId, int? countryId, int? cityId, string? contact, string? email, string? address, string? ntnNumber, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACBranch_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@OrganisationTypeId", (object)organisationTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CountryId", (object)countryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CityId", (object)cityId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object)address! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NTNNumber", (object)ntnNumber! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);
            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ACUser(string? operationType, Guid? guId, string? description, string? password, string? contact, string? email, int? employeeId, int? roleId, string? allowedBranchIds, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ACUser_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Password", (object)password! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Contact", (object)contact! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)email! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EmployeeId", (object?)employeeId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RoleId", (object?)roleId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AllowedBranchIds", (object?)allowedBranchIds ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_CSDepartment(string? operationType, Guid? guId,  string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("CSDepartment_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ISection(string? operationType, Guid? guId, string? description, int? departmentId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ISection_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ICategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ICategory_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object)sectionId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_ISubCategory(string? operationType, Guid? guId, string? description, int? departmentId, int? sectionId, int? categoryId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("ISubCategory_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", (object)departmentId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object)sectionId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CategoryId", (object)categoryId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_IBrand(string? operationType, Guid? guId, string? description, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IBrand_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object)description! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_IProduct(string? operationType, Guid? guId, string? description, string? machineNumber, string? sku, string? additionalDetail, string? attributeIds, int? brandId, bool? isFavorite, bool? isSaleTaxExclusive, int? departmentId, int? sectionId, int? categoryId, int? subCategoryId, decimal? criticalLimit, int? saleUnitId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, int? branchId, int? companyId, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IProduct_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object?)operationType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object?)guId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", (object?)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MachineNumber", (object?)machineNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SKU", (object?)sku ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AdditionalDetail", (object?)additionalDetail ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AttributeIds", (object?)attributeIds ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BrandId", (object?)brandId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsFavorite", (object?)isFavorite ?? false);
            cmd.Parameters.AddWithValue("@IsSaleTaxExclusive", (object?)isSaleTaxExclusive ?? false);
            cmd.Parameters.AddWithValue("@DepartmentId", (object?)departmentId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SectionId", (object?)sectionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CategoryId", (object?)categoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SubCategoryId", (object?)subCategoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CriticalLimit", (object?)criticalLimit ?? 0m);
            cmd.Parameters.AddWithValue("@SaleUnitId", (object?)saleUnitId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object?)createdOn ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            cmd.Parameters.AddWithValue("@BranchId", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CompanyId", (object?)companyId ?? DBNull.Value);

            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();

            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
        public async Task<int?> UpsertInto_IProductATI(string? operationType, Guid? guId,int? productId,int?inventoryAccountId,int?saleRevenueAccountId,int? costOfSaleAccountId,int? itemTypeId,int? hsCodeId, int? saleTaxTypeId, DateTime? createdOn, int? createdBy, DateTime? updatedOn, int? updatedBy, int? documentType, int? documentStatus, SqlConnection con, SqlTransaction trans)
        {
            using var cmd = new SqlCommand("IProductATI_Upsert", con, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DB_OperationType", (object)operationType! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GuID", (object)guId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProductId", (object)productId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@InventoryAccountId", (object)inventoryAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SaleRevenueAccountId", (object)saleRevenueAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@costOfSaleAccountId", (object)costOfSaleAccountId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ItemTypeId", (object)itemTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HSCodeId", (object)hsCodeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SaleTaxTypeId", (object)saleTaxTypeId! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedOn", (object)createdOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", (object?)createdBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedOn", (object?)updatedOn! ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedBy", (object?)updatedBy! ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentType", (object?)documentType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DocumentStatus", (object?)documentStatus ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", true);
            var responseParam = new SqlParameter("@Response", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(responseParam);

            await cmd.ExecuteNonQueryAsync();
            return responseParam.Value == DBNull.Value ? null : (int?)responseParam.Value;
        }
    }
}
