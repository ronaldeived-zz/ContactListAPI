namespace ContactAPI.ViewModel
{
    public class PersonViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Picture { get; set; }
        public ICollection<ContactViewModel>? Contacts { get; set; }
    }
}