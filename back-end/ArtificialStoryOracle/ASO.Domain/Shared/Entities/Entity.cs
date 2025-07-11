﻿using ASO.Domain.Shared.Events.Abstractions;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Shared.Entities;

public abstract class Entity : IEquatable<Guid>
{
    #region private fields
    
    private readonly List<IDomainEvent> _events = new();
    
    #endregion
    
    #region Properties
    public Guid Id { get; } = Guid.NewGuid();
    public Tracker Tracker { get; } = Tracker.Create();
    
    #endregion

    #region Equatable Implementation

    public bool Equals(Guid id) => Id == id;
    
    public override int GetHashCode() => Id.GetHashCode();

    #endregion
    
    public IReadOnlyCollection<IDomainEvent> Events => _events;
    public void ClearEvents() => _events.Clear();
    public void RaiseEvent(IDomainEvent @event) => _events.Add(@event);
}