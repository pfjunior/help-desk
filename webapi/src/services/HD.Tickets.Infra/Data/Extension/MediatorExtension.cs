﻿using HD.Core.DomainObjects;
using HD.Core.Mediator;
using Microsoft.EntityFrameworkCore;

namespace HD.Tickets.Infra.Data.Extension;

public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : DbContext
    {
        var domainEntities = context.ChangeTracker.Entries<Entity>().Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var domainEvents = domainEntities.SelectMany(x => x.Entity.Notifications).ToList();

        domainEntities.ToList().ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents.Select(async (domainEvent) => { await mediator.PublishEvent(domainEvent); });

        await Task.WhenAll(tasks);
    }
}
