using ContactAPI.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Models
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "The First Name is required", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The First name must have a minimal 5 and max 50 characters.")]
        public string FirstName { get; set; }

        public static explicit operator Person(PersonViewModel v)
        {
            throw new NotImplementedException();
        }

        [Required(ErrorMessage = "The Last Name is required", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Last name must have a minimal 5 and max 50 characters.")]
        public string LastName { get; set; } 
        public string? Picture { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdate { get; set; }

        public Person(string firstName, string lastName)
        {
            PersonId = Guid.NewGuid();
            FirstName = Validate(firstName);
            LastName = Validate(lastName);
        }

        public string Validate(string? value)
        {
            if (value == null) throw new Exception($"Value {value} can't be null");

            return value;
        }
    }
}