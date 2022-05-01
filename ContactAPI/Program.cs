using ContactAPI.Data;
using ContactAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBConnections")));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(services => services.AddPolicy("MySpecifOrigin", police =>
{
    police.WithOrigins("https://listcontact.azurewebsites.net").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MySpecifOrigin");

app.MapGet("/people", async (ContactContext _context) => await _context.Person.ToListAsync());

app.MapGet("/person/{id}", async (int id, ContactContext _context) => await _context.Person.FindAsync(id));

app.MapDelete("/person/{id}", async (int id, ContactContext _context) => {
    var person = await _context.Person.FindAsync(id);
    if (person != null)
    {
        _context.Person.Remove(person);
        await _context.SaveChangesAsync();
    }
    return Results.NoContent();
});

app.MapPut("/person/{id}", async (int id, Person person ,ContactContext _context) => {
    _context.Entry(person).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return person;
});

app.MapPost("/person", async (Person person, ContactContext _context) => {
    _context.Person.AddAsync(person);
    await _context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("{idPerson}/contacts", async (int idPerson, ContactContext _context) => await _context.Contact.Where(c => c.PersonId == idPerson).ToListAsync());

app.MapGet("{idPerson}/contact/{contactId}", async (int idPerson, int contactId, ContactContext _context) => await _context.Contact.FirstOrDefaultAsync(c => c.PersonId == idPerson && c.ContactId == contactId));

app.MapDelete("{idPerson}/contact/{contactId}", async (int idPerson, int contactId, ContactContext _context) => {
    var contact = await _context.Contact.FirstOrDefaultAsync(c => c.PersonId == idPerson && c.ContactId == contactId);
    if (contact != null)
    {
        _context.Contact.Remove(contact);
        await _context.SaveChangesAsync();
    }
    return Results.NoContent();
});

app.MapPut("/contact/{id}", async (int id, Contact contact, ContactContext _context) =>
{
    _context.Entry(contact).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return contact;
});

app.MapPost("/contact", async (Contact contact, ContactContext _context) =>
{
    _context.Contact.AddAsync(contact);
    await _context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
