using ContactAPI.Models.Enums;

namespace ContactAPI.Models
{
    public class Contact
    {
        public int? Id { get; set; }
        public ETypeContact? Type { get; set; }
        public string? Value { get; set; }
    }
}