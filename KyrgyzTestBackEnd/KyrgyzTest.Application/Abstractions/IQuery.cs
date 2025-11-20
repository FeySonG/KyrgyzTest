using MediatR;

namespace KyrgyzTest.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse>;