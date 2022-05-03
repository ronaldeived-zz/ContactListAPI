using ContactAPI.Models.Enums;

namespace ContactAPI.ViewModel
{
    public class ContactViewModel
    {
        public ETypeContact Type { get; set; }
        public string Value { get; set; }
    }
}