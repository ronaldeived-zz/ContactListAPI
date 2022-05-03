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

        public ContactViewModel ContactToContactViewModel(Contact contact)
        {
            return new ContactViewModel()
            {
                Type = contact.Type,
                Value = contact.Value
            };
        }

        public ICollection<Contact> ContactsViewModelToContact(ICollection<ContactViewModel> contactsViewModel)
        {
            List<Contact> list = new List<Contact>();
            try
            {
                foreach (var contactViewModel in contactsViewModel) list.Add(ContactViewModelToContact(contactViewModel));
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public Contact ContactFromDBUpdateData(Contact updatedContact, Contact contactFromDB)
        {
            updatedContact.PersonId = contactFromDB.PersonId;
            updatedContact.ContactId = contactFromDB.ContactId;
            updatedContact.DateCreated = contactFromDB.DateCreated;
            return updatedContact;
        }
    }
}
