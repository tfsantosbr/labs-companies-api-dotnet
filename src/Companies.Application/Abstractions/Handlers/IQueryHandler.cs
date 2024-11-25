﻿using Companies.Application.Abstractions.Results;

namespace Companies.Application.Abstractions.Handlers;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
