using ContactAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAPI.Models
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        [Required(ErrorMessage = "The Type is required", AllowEmptyStrings = false)]
        public ETypeContact Type { get; set; }

        [Required(ErrorMessage = "The Last Name is required", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Value must have a minimal 5 and max 50 characters.")]
        public string Value { get; set; }
        public Guid PersonId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdate { get; set; }

        public Contact(ETypeContact type, string value)
        {
            ContactId = Guid.NewGuid();
            Type = (ETypeContact) Validate((int) type);
            Value = Validate(value);
        }

        public string Validate(string? value)
        {
            if (value == null) throw new Exception($"Value {value} can't be null");

            return value;
        }

        public int Validate(int? value)
        {
            if (value == null) throw new Exception($"Value Type can't be null");

            return (int) value;
        }
    }
}