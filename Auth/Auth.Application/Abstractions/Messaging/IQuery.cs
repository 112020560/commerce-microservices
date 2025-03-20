using MediatR;
using SharedKernel;

namespace Auth.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;