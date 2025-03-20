using MediatR;
using SharedKernel;

namespace Inventories.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;