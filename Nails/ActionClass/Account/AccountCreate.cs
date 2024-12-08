using System.ComponentModel.DataAnnotations;

namespace Nails.ActionClass.Account
{
    public class AccountCreate
    {
        [Required]
        public string Name { get; internal set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
    }
}
