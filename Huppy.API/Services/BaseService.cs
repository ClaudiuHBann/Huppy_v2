using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Utilities;

namespace Huppy.API.Services
{
public class BaseService<Type>(ILogger<Type> logger, HuppyContext context)
{
    public string LastError { get; private set; } = "";

    protected void ClearLastError() => LastError = "";
    protected void SetLastError(string error) => LastError = error;

    protected async Task<EntityType?> FindByKeys<EntityType>(params object?[]? keyValues)
        where EntityType : class => await context.FindAsync(typeof(EntityType), keyValues) as EntityType;

  protected async Task<EntityType ?> Create<EntityType>(EntityType entity)
        where EntityType : class
    {
        var propertyID = entity.GetType().GetProperty("Id");
        if (propertyID == null)
        {
            return null;
        }

        var dbSet = GetDbSet<EntityType>();
        if (dbSet == null)
        {
            return null;
        }

        propertyID.SetValue(entity, await dbSet.CountAsync() + 1);
        await dbSet.AddAsync(entity);
        return await context.SaveChangesAsync() > 0 ? entity : null;
    }

    private DbSet<T>? GetDbSet<T>()
        where T : class
    {
        foreach (var property in context.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                property.PropertyType.GetGenericArguments()[0] == typeof(T))
            {
                return (DbSet<T>?)property.GetValue(context);
            }
        }

        return null;
    }

    /// <summary>
    /// Finds the entity by it's id and updates all it's properties
    /// </summary>
    /// <returns>true even if no properties changed</returns>
    protected async Task<EntityType?> Update<EntityType>(EntityType? entity)
        where EntityType : class
    {
        if (entity == null)
        {
            return null;
        }

        var propertyID = entity.GetType().GetProperty("Id");
        if (propertyID == null)
        {
            return null;
        }

        var propertyIDValue = propertyID.GetValue(entity);
        if (propertyIDValue == null)
        {
            return null;
        }

        var entityUpdated = await FindByKeys<EntityType>(propertyIDValue);
        if (entityUpdated == null)
        {
            return null;
        }

        var entityUpdatedProperties = entityUpdated.GetType().GetProperties();
        var entityProperties = entity.GetType().GetProperties();

        // if we update properties with the sanem value, SaveChangesAsync will return 0 so:
        int equalCount = 0;

        for (int i = 0; i < entityProperties.Length; i++)
        {
            var entityUpdatedPropertyValue = entityUpdatedProperties[i].GetValue(entityUpdated);
            var entityPropertyValue = entityProperties[i].GetValue(entity);

            // check if they are equal enumerables
            if (entityUpdatedPropertyValue != null && entityPropertyValue != null)
            {
                var isEnumerable = entityPropertyValue.GetType().GetInterfaces().Any(
                    t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                if (isEnumerable)
                {
                    // TODO: better way of checking this?
                    equalCount += entityPropertyValue.ToJSON() == entityUpdatedPropertyValue.ToJSON() ? 1 : 0;
                }
            }

            if (entityUpdatedPropertyValue == entityPropertyValue)
            {
                equalCount++;
            }

            entityUpdatedProperties[i].SetValue(entityUpdated, entityPropertyValue);
        }

        var updated = await context.SaveChangesAsync() > 0 || equalCount > 0;
        return updated ? entityUpdated : null;
    }
}
}
