using System;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Persons.Create;
using Domain.Models.crm;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Persons.Create;

public class CreatePersonCommandHadler : ICommandHandler<CreatePersonCommand, ResponseObject>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CreatePersonCommandHadler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<ResponseObject>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var person = await _dbContext.Query<Person>().FirstOrDefaultAsync(x => x.Id == request.PersonRequest.Id, cancellationToken);

        var attributes = request.PersonRequest.PersonAttributtes != null ?
            request.PersonRequest.PersonAttributtes.Select(att => new Domain.Models.crm.Attribute
            {
                Value = att.Value,
                DataType = att.DataType,
                Key = att.Key,

            }) : [];

            var addressList = request.PersonRequest.PersonAddresses != null ?
            request.PersonRequest.PersonAddresses.Select(att => new Domain.Models.crm.Address
            {
                City = att.City,
                Country = att.Country,
                State = att.State,
                PostalCode = att.ZipCode,
                Address1 = $"{att.Street} - {att.Neighborhood} - {att.Complement}",
                 Type = att.Type
            }) : [];

        if (person is null)
        {
            var newPerson = new Person
            {
                FirstName = request.PersonRequest.FirstName,
                LastName = request.PersonRequest.LastName,
                Email = request.PersonRequest.Email,
                Phone = request.PersonRequest.Phone,
                BirthDate = request.PersonRequest.BirthDate,
                CreateAt = _dateTimeProvider.UtcNow,
                UpdateAt = _dateTimeProvider.UtcNow,
                Attributes = [.. attributes],
                Addresses = [..addressList],
                TypeId = request.PersonRequest.TypeId,
                StatusId = request.PersonRequest.StatusId
            };

            await _dbContext.Person.AddAsync(newPerson, cancellationToken);
        }
        else {
            person.FirstName = request.PersonRequest.FirstName;
            person.LastName = request.PersonRequest.LastName;
            person.Email = request.PersonRequest.Email;
            person.Phone = request.PersonRequest.Phone;
            person.BirthDate = request.PersonRequest.BirthDate;
            person.UpdateAt = _dateTimeProvider.UtcNow;
            person.Attributes = [.. attributes];
            person.Addresses = [..addressList];
            _dbContext.Person.Update(person);
        }


        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<ResponseObject>(new ResponseObject
        {
            IsSuccess = true,
            Message = "Person created successfully",
            Timestamp = _dateTimeProvider.UtcNow
        }, true, Error.None);
    }
}
