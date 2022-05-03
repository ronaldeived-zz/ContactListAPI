using ContactAPI.Data;
using ContactAPI.Mapping;
using ContactAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBConnections")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(services => services.AddPolicy("MySpecifOrigin", police =>
{
    police.WithOrigins(builder.Configuration.GetSection("HostFrontEnd").Value).AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MySpecifOrigin");

app.MapGet("/people", async (ContactContext _context) => await _context.Person.ToListAsync());

app.MapGet("/person/{personId}", async (Guid personId, ContactContext _context) => await _context.Person.FirstOrDefaultAsync(p=> p.PersonId == personId));

app.MapDelete("/person/{personId}", async (Guid personId, ContactContext _context) =>
{
    var person = await _context.Person.FirstOrDefaultAsync(p=> p.PersonId == personId);
    if (person != null)
    {
        _context.Person.Remove(person);
        await _context.SaveChangesAsync();
    }
    return Results.NoContent();
});

app.MapPut("/person/{personId}", async (Guid personId, PersonViewModel personViewModel, ContactContext _context) =>
{
    PersonMapping personMapping = new PersonMapping();

    var personfromDatabase = await _context.Person.FirstOrDefaultAsync(c => c.PersonId == personId);
    var currentPerson = personMapping.PersonViewModelToPerson(personViewModel);

    currentPerson = personMapping.PersonFromDBUpdateData(currentPerson, personfromDatabase);

    _context.ChangeTracker.Clear();

    _context.Entry(currentPerson).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return currentPerson;


});

app.MapPost("/person", async (PersonViewModel person, ContactContext _context) =>
{
    PersonMapping personMapping = new PersonMapping();
    var newPerson = personMapping.PersonViewModelToPerson(person);
    await _context.Person.AddAsync(newPerson);
    await _context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("{idPerson}/contacts", async (Guid idPerson, ContactContext _context) => await _context.Contact.Where(c => c.PersonId == idPerson).ToListAsync());

app.MapGet("/contact/{contactId}", async (Guid contactId, ContactContext _context) => await _context.Contact.FirstOrDefaultAsync(c => c.ContactId == contactId));

app.MapDelete("/contact/{contactId}", async (Guid contactId, ContactContext _context) =>
{
    var contact = await _context.Contact.FirstOrDefaultAsync(c => c.ContactId == contactId);
    if (contact != null)
    {
        _context.Contact.Remove(contact);
        await _context.SaveChangesAsync();
    }
    return Results.NoContent();
});

app.MapPut("/contact/{contactId}", async (Guid contactId, ContactViewModel contact, ContactContext _context) =>
{
    ContactMapping contactMapping = new ContactMapping();
    var currentContact = contactMapping.ContactViewModelToContact(contact);
    var contactfromDatabase = await _context.Contact.FirstOrDefaultAsync(c => c.ContactId == contactId);

    currentContact = contactMapping.ContactFromDBUpdateData(currentContact, contactfromDatabase);

    _context.ChangeTracker.Clear();

    _context.Entry(currentContact).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return contact;
});

app.MapPost("{personId}/contact", async (Guid personId, ContactViewModel contact, ContactContext _context) =>
{
    ContactMapping contactMapping = new ContactMapping();
    var newContact = contactMapping.ContactViewModelToContact(contact);
    if (await _context.Person.FindAsync(personId) != null)
    {
        newContact.PersonId = personId;
        await _context.Contact.AddAsync(newContact);
        await _context.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.BadRequest("There is no person to add a contact.");
});

app.Run();
