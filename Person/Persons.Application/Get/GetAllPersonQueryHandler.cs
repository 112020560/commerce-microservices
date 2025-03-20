using Application.Persons.Get;
using Microsoft.EntityFrameworkCore;
using Persons.Application.Abstractions.Data;
using Persons.Application.Abstractions.Messaging;
using Persons.Domain.Entities;
using SharedKernel;

namespace Persons.Application.Persons.Get;

internal sealed class GetAllPersonQueryHandler : Utility, IQueryHandler<GetAllPersonsQuery, ResponseObject<List<CustomPersonResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    public GetAllPersonQueryHandler(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<ResponseObject<List<CustomPersonResponse>>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.Request.PageNumber;
        var pageSize = request.Request.PageSize;
        var allPerson = await _dbContext.Query<Person>().AsNoTracking()
                                                    .OrderBy(e => e.Id)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync(cancellationToken: cancellationToken);
        var personsResponse = ParseEntityToResponse(allPerson);
        var response = new ResponseObject<List<CustomPersonResponse>>
        { 
            Data = personsResponse, 
            IsSuccess = true, 
            Timestamp= _dateTimeProvider.UtcNow, 
            Message = personsResponse?.Count == 0 ?"Not found results" : $"{personsResponse?.Count} Person found" };
        return Result.Success(response);
    }
}
