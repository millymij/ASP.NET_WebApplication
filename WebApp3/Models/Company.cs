namespace WebApplication3.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // navigation property
        // collection of addresses associated w a company
        public virtual ICollection<Address> AddressList { get; set; }
    }
}
