using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkBackAccessControl.Data.Models
{
    //[NotMapped]
    [Table("User")]

    public partial class DtoUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("userName")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The user name should be between 3 characters to 15")]
        [Required(ErrorMessage = "Username is a required field")]
        public string UserName { get; set; } = null!;
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The password should be between 3 characters to 15")]
        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; } = null!;
    }

}
