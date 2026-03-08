using System;
using System.Collections.Generic;

namespace GateWay.Models.DAL;

public partial class vSaleTaxType
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? DefaultRate { get; set; }

    public decimal? AdditionalRate { get; set; }
}
