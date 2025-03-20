using Microsoft.EntityFrameworkCore;
using Persons.Application.Abstractions.Data;
using Persons.Application.Abstractions.Messaging;
using Persons.Domain.Entities;
using SharedKernel;

namespace Persons.Application.Get;

internal sealed class GetPersonByFilterQueryHandler : Utility, IQueryHandler<GetPersonByFilterQuery, ResponseObject<List<CustomPersonResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    public GetPersonByFilterQueryHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<ResponseObject<List<CustomPersonResponse>>>> Handle(GetPersonByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = BuildFilter(request.Request);
        var query = _dbContext.Query<Person>().AsNoTracking().Where(filter);
        var totalItems = await query.CountAsync(cancellationToken);

        var queryResult = await query
                                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                                .Take(request.Request.PageSize)
                                .ToListAsync(cancellationToken);

        var response = new ResponseObject<List<CustomPersonResponse>>
        {
            Data = ParseEntityToResponse(queryResult),
            IsSuccess = true,
            Timestamp = _dateTimeProvider.UtcNow,
            Message = totalItems == 0 ? "Not found results" : $"{totalItems} Person found",
            TotalsRecords = totalItems
        };
        return Result.Success(response);
    }
}
