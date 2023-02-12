using DDD.Shared.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Shared.Infrastructure.Core.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, EFContext ctx)
        {
            var domainEntities = ctx.ChangeTracker.Entries<Entity>().Where(n => n.Entity.DomainEvents != null && n.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities.Select(n => n.Entity.GetDomainEvents()).SelectMany(n => n).ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
