using System;
using System.Collections.Generic;

namespace IMSClothHouse.Models.DAL;

public partial class CSaleUnit
{
    public int Id { get; set; }

    public Guid? GuID { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public decimal? Packing { get; set; }

    public decimal? PackingQuantity { get; set; }

    public int? UnitId { get; set; }

    public bool? IsFixed { get; set; }

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
