using WebApplication3.Models;

namespace WebApplication3
{
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetAddress1 { get; set; } = string.Empty;
        public string StreetAddress2 { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;

        // foreign key
        public int CompanyId { get; set; }
        public int CountryId { get; set; }

        // navigation property
        // each address to a single company
        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
