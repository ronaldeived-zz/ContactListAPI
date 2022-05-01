using ContactAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAPI.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public ETypeContact? Type { get; set; }
        public string? Value { get; set; }
        public int PersonId { get; set; }
    }
}