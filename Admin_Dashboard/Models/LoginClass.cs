using System.ComponentModel.DataAnnotations;

namespace Admin_Dashboard.Models
{
    public class LoginClass
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="User name is required")]
        public string  UserName { get; set; }
        [Required]
        [DataType(DataType.Password,ErrorMessage ="Password must be Number")]   
        public string Password { get; set; }
        [Required]
        [DataType( DataType.Password)]
        [Compare("Password" ,ErrorMessage ="Password and confirm password must match")]
        public string ConfirmPassword { get; set; } 
    }
}
