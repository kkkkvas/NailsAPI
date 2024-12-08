using System.ComponentModel.DataAnnotations;

namespace Nails.ActionClass.HelperClass.DTO
{
    public class ClientDTO
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
