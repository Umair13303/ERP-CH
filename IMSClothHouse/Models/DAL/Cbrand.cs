using System;
using System.Collections.Generic;

namespace IMSClothHouse.Models.DAL;

public partial class Cbrand
{
    public int Id { get; set; }

    public Guid? GuId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? FocalPerson { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DocType { get; set; }

    public int? DocumentStatus { get; set; }

    public bool? Status { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }
}
