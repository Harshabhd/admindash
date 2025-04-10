using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Dashboard.Models
{
    public class CompanyUsers
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage ="User name is must")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First name is must")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is must")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "PAssword name is must")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Invaild Email Address")]
        [EmailAddress(ErrorMessage ="Invaild Email Address")]
        public string Email { get; set; }
        [Display(Name = "Admin Access")]
        public bool  AdminAccess { get; set; }

        [Required]
        public int CompanyId { get; set; }
        
    }
}
