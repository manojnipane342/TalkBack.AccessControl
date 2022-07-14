using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkBack.Contacts.Data.Models
{
    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is a required field")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The name should be between 3 characters to 25")]
        public string DisplayName { get; set; } = null!;
        public bool? Status { get; set; }
        public Guid? UserId { get; set; }

    }
}
