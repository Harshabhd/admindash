using System.ComponentModel.DataAnnotations;

namespace Admin_Dashboard.Models
{
    public class Company
    {
        [Key] 
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime EstablishDate { get; set; }
        public string CompanyDetails { get; set; }

    }
}
