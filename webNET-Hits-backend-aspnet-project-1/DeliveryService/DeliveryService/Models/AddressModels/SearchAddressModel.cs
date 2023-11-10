using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace DeliveryService.Models.AddressModles
{
    public class SearchAddressModel
    {
        [Key]
        public long Objectid { get; set; }
        public Guid ObjectGuid { get; set; }  
        public string? text { get; set; }
        public GarAddressLevel objectLevel { get; set; }
        public string? objectLevelText { get; set; }

        public SearchAddressModel(long objectid, Guid objectGuid, string text, GarAddressLevel objectLevel, string objectLevelText)
        {
            Objectid = objectid;
            ObjectGuid = objectGuid;
            this.text = text;
            this.objectLevel = objectLevel;
            this.objectLevelText = objectLevelText;
        }

        public SearchAddressModel() { }
    }
}
