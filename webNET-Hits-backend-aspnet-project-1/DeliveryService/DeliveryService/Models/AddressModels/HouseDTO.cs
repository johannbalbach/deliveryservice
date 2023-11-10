using System;
using System.Collections.Generic;

namespace DeliveryService.Models.AddressModles;

public partial class HouseDTO
{
    public long Id { get; set; }

    public long Objectid { get; set; }

    public Guid ObjectGuid { get; set; }

    public string Housenum { get; set; } = null!;

    public string? Addnum1 { get; set; }

    public string? Addnum2 { get; set; }

    public int Addtype1 { get; set; }

    public int Addtype2 { get; set; }

    public int IsActive { get; set; }
}
