using System;
using System.Collections.Generic;

namespace DeliveryService.Models.AddressModles;

public partial class HierarchyDTO
{
    public long Id { get; set; }

    public long ObjectId { get; set; }

    public long ParentObjectId { get; set; }

    public int IsActive { get; set; }
}
