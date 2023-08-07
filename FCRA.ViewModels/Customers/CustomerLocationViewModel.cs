using FCRA.Common;
using FCRA.ViewModels.Masters;
using System.ComponentModel.DataAnnotations;

namespace FCRA.ViewModels.Customers
{
    public class CustomerLocationViewModel
    {
        [MapToDTO]
        public int LocationId { get; set; }
        [MapToDTO]
        public int CustomerId { get; set; }
        [MapToDTO, Display(Name = "Country")]
        public int CountryId { get; set; }
        [MapToDTO, Required, MaxLength(200), Display(Name = "Location Name")]
        public string? LocationName { get; set; }
        [MapToDTO, Required, MaxLength(200), Display(Name = "Address 1")]
        public string? Address1 { get; set; }
        [MapToDTO, MaxLength(200), Display(Name = "Address 2")]
        public string? Address2 { get; set; }
        [MapToDTO, MaxLength(200), Display(Name = "Address 3")]
        public string? Address3 { get; set; }
        [MapToDTO, MaxLength(200), Display(Name = "Address 4")]
        public string? Address4 { get; set; }

        public virtual CustomerViewModel? Customer { get; set; }
        public virtual GeographyRiskViewModel? Country { get; set; }
    }
}
