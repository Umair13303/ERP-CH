using System;
using System.Collections.Generic;

namespace OrganisationSetup.Models.DAL;

public partial class AFCustomerLedger
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public int? Code { get; set; }

    public int? TransactionTypeId { get; set; }

    public string? DocumentReferance { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Description { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocumentType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
