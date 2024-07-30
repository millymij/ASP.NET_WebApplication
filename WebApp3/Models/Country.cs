namespace WebApplication3.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; } = string.Empty;

        // navigation property
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
