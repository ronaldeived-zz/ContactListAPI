﻿using ContactAPI.Models;
using ContactAPI.ViewModel;

namespace ContactAPI.Mapping
{
    public class PersonMapping
    {
        public Person PersonViewModelToPerson(PersonViewModel personViewModel)
        {
            ContactMapping contactMapping = new ContactMapping();
            return new Person(personViewModel.FirstName, personViewModel.LastName)
            {
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName,
                Picture =  personViewModel.Picture,
                Contacts = contactMapping.ContactsViewModelToContact(personViewModel.Contacts)
            };
        }
    }
}
