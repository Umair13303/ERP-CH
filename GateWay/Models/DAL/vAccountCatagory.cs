using System;
using System.Collections.Generic;

namespace GateWay.Models.DAL;

public partial class vAccountCatagory
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? ShortCode { get; set; }

    public int? AccountTypeId { get; set; }
}
