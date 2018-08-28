using System.ComponentModel.DataAnnotations;

namespace Unidays.Interview.UI.ViewModels
{
    public class UserCreationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [MaxLength(100)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UnencryptedPassword { get; set; }
    }
}
