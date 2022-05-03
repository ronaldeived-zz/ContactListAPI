using ContactAPI.Models;
using ContactAPI.ViewModel;

namespace ContactAPI.Mapping
{
    public class ContactMapping
    {
        public Contact ContactViewModelToContact(ContactViewModel contactViewModel)
        {
            return new Contact(contactViewModel.Type, contactViewModel.Value)
            {
                Type = contactViewModel.Type,
                Value = contactViewModel.Value
            };
        }

        public ICollection<Contact> ContactsViewModelToContact(ICollection<ContactViewModel> contactsViewModel)
        {
            List<Contact> list = new List<Contact>();
            foreach (var contactViewModel in contactsViewModel) list.Add(ContactViewModelToContact(contactViewModel));
            return list;
        }
    }
}
