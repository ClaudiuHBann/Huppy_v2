using System.Net;
using System.Reflection;

using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Utilities;
using Shared.Exceptions;

namespace Huppy.API.Services
{
public class BaseService<Entity>(HuppyContext context)
    where Entity : class
{
    protected virtual async Task CreateValidate(Entity entity) => await Exists(entity);
    protected virtual async Task ReadValidate(Entity entity) => await Exists(entity);
    protected virtual async Task UpdateValidate(Entity entity) => await Exists(entity);
    protected virtual async Task DeleteValidate(Entity entity) => await Exists(entity);

    public virtual async Task<Entity> Create(Entity entity) => await CRUD(entity, EAction.Create);
    public virtual async Task<Entity> Read(Entity entity) => await CRUD(entity, EAction.Read);
    public virtual async Task<Entity> Update(Entity entity) => await CRUD(entity, EAction.Update);
    public virtual async Task<Entity> Delete(Entity entity) => await CRUD(entity, EAction.Delete);

    private enum EAction
    {
        Create,
        Read,
        Update,
        Delete
    }

    private async Task<Entity> CRUD(Entity entity, EAction action)
    {
        var methodValidation =
            Invoke<Task>($"{action}Validate", [entity]) ??
            throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                            $"Failed {action.ToString().ToLower()} validation for {entity}!"));
        await methodValidation;

        var methodCRUD = Invoke<Task<Entity>>($"{action}Ex", [entity]) ??
                         throw new DatabaseException(new(HttpStatusCode.InternalServerError,
                                                         $"Failed to {action.ToString().ToLower()} {entity}!"));
        return await methodCRUD;
    }

    private Result? Invoke<Result>(string method, object?[]? parameters)
        where Result : class
    {
        var methodValidation = GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic);
        if (methodValidation == null)
        {
            return null;
        }

        var methodValidationResult = methodValidation.Invoke(this, parameters);
        if (methodValidationResult == null)
        {
            return null;
        }

        return methodValidationResult as Result;
    }

    private DbSet<Entity> GetDbSet()
    {
        var exception = new DatabaseException(
            new(HttpStatusCode.InternalServerError, $"Failed to get the database set for {typeof(Entity)}."));

        foreach (var property in context.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                property.PropertyType.GetGenericArguments()[0] == typeof(Entity))
            {
                return (DbSet<Entity>?)property.GetValue(context) ?? throw exception;
            }
        }

        throw exception;
    }

    protected async Task<Entity> CreateEx(Entity entity)
    {
        var propertyID = entity.GetType().GetProperty("Id");
        propertyID?.SetValue(entity, Guid.NewGuid());

        await GetDbSet().AddAsync(entity);
        return await context.SaveChangesAsync() > 0
                   ? entity
                   : throw new DatabaseException(new(HttpStatusCode.BadRequest, $"Failed to create {entity}!"));
    }

    protected async Task<Entity> ReadEx(params object?[]? keyValues)

        => await context.FindAsync(typeof(Entity), keyValues) as Entity ??
           throw new DatabaseException(new(HttpStatusCode.NotFound, $"The {typeof(Entity)} could not be found!"));

    /// <summary>
    /// Finds the entity by it's id and updates all it's properties
    /// </summary>
    /// <returns>true even if no properties changed</returns>
    protected async Task<Entity> UpdateEx(Entity entity)
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);
        var entityUpdated = await ReadEx(propertyIDValue);
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
                    equalCount += entityPropertyValue.ToMsgPack() == entityUpdatedPropertyValue.ToMsgPack() ? 1 : 0;
                }
            }

            if (entityUpdatedPropertyValue == entityPropertyValue)
            {
                equalCount++;
            }

            entityUpdatedProperties[i].SetValue(entityUpdated, entityPropertyValue);
        }

        var updated = await context.SaveChangesAsync() > 0 || equalCount > 0;
        return updated ? entityUpdated
                       : throw new DatabaseException(new(HttpStatusCode.BadRequest, $"Failed to update the {entity}!"));
    }

    protected async Task<Entity> DeleteEx(Entity entity)
    {
        GetDbSet().Remove(entity);
        return await context.SaveChangesAsync() > 0
                   ? entity
                   : throw new DatabaseException(new(HttpStatusCode.BadRequest, $"Failed to delete {entity}!"));
    }

    private async Task<bool> Exists(Entity entity)
    {
        var propertyID = entity.GetType().GetProperty("Id");
        var propertyIDValue = propertyID?.GetValue(entity);
        return await ReadEx(propertyIDValue) != null;
    }
}
}
