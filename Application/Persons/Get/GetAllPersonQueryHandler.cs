using System;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Persons.Get;

internal sealed class GetAllPersonQueryHandler : IQueryHandler<GetAllPersonsQuery, ResponseObject<CustomPersonResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllPersonQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public async Task<Result<ResponseObject<CustomPersonResponse>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var person
    }
}
