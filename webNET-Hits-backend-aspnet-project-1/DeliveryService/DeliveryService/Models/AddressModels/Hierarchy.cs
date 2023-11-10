using System;
using System.Collections.Generic;

namespace DeliveryService.Models.AddressModles;

public partial class Hierarchy
{
    public long Id { get; set; }
    public long? Objectid { get; set; }
    public long? Parentobjid { get; set; }
    public long? Changeid { get; set; }
    public string? Regioncode { get; set; }
    public string? Areacode { get; set; }
    public string? Citycode { get; set; }
    public string? Placecode { get; set; }
    public string? Plancode { get; set; }
    public string? Streetcode { get; set; }
    public long? Previd { get; set; }
    public long? Nextid { get; set; }
    public DateOnly? Updatedate { get; set; }
    public DateOnly? Startdate { get; set; }
    public DateOnly? Enddate { get; set; }
    public int? Isactive { get; set; }
    public string? Path { get; set; }
}
