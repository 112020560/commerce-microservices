using System;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Person.Create;

public class CreatePersonCommandHadler : ICommandHandler<CreatePersonCommand, ResponseObject>
{
    private readonly IApplicationDbContext _dbContext;
    public CreatePersonCommandHadler(IApplicationDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public async Task<Result<ResponseObject>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Query<Person>().AnyAsync()
    }
}
