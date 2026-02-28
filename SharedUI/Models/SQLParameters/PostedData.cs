using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.SQLParameters
{
    public class PostedData
    {
        public string? OperationType { get; set; }
        public Guid? GuID { get; set; }
        public string? Description { get; set; }
        public int? OrganisationTypeId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? CategoryId { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Logo { get; set; }
        public string? NTNNumber { get; set; }
        public string? Password { get; set; }
        public string? AllowedBranchIds { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DocType { get; set; }
        public int? DocumentStatus { get; set; }
        public bool? Status { get; set; }
        public int? BranchId { get; set; }
        public int? CompanyId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
    }
}
