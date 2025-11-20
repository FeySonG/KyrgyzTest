using MediatR;

namespace KyrgyzTest.Application.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest;