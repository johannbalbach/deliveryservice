using System;
using System.Collections.Generic;

namespace DeliveryService.Models.AddressModles;

public partial class AddressDTO
{
    public long Id { get; set; }

    public long Objectid { get; set; }

    public Guid ObjectGuid { get; set; }

    public string Name { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public int Level { get; set; }

    public int IsActive { get; set; }
}
