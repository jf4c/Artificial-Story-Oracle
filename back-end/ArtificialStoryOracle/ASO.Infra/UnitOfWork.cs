﻿using ASO.Domain.Shared.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Infra.Database;
using MediatR;

namespace ASO.Infra;

public sealed class UnitOfWork(AppDbContext context, IMediator mediator) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly IMediator _mediator = mediator;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Events)
            .ToList();

        var result = await _context.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        domainEntities.ForEach(entity => entity.ClearEvents());

        return result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

