using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Picture { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
    }
}