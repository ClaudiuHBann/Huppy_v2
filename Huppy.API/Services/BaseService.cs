using System.Reflection;

using Huppy.API.Models;

using Microsoft.EntityFrameworkCore;

using Shared.Utilities;

namespace Huppy.API.Services
{
public class BaseService<Entity>(HuppyContext context)
    where Entity : class
{
    public string LastError { get; private set; } = "";

    protected void ClearLastError() => LastError = "";
    protected void SetLastError(string error) => LastError = error;

    protected virtual async Task<bool> CreateValidate(Entity? entity) => !await Exists(entity);
    protected virtual async Task<bool> ReadValidate(Entity? entity) => await Exists(entity);
    protected virtual async Task<bool> UpdateValidate(Entity? entity) => await Exists(entity);
    protected virtual async Task<bool> DeleteValidate(Entity? entity) => await Exists(entity);

    public virtual async Task<Entity?> Create(Entity? entity) => await CRUD(entity, EAction.Create);
    public virtual async Task<Entity?> Read(Entity? entity) => await CRUD(entity, EAction.Read);
    public virtual async Task<Entity?> Update(Entity? entity) => await CRUD(entity, EAction.Update);
    public virtual async Task<Entity?> Delete(Entity? entity) => await CRUD(entity, EAction.Delete);

    private enum EAction
    {
        Create,
        Read,
        Update,
        Delete
    }

    private async Task<Entity?> CRUD(Entity? entity, EAction action)
    {
        ClearLastError();

        if (entity == null)
        {
            return null;
        }

        var methodValidation = Invoke<Task<bool>>($"{action}Validate", [entity]);
        if (methodValidation == null)
        {
            return null;
        }

        if (!await methodValidation)
        {
            if (string.IsNullOrWhiteSpace(LastError))
            {
                SetLastError($"Failed {action.ToString().ToLower()} validation for {entity}!");
            }

            return null;
        }

        var methodCRUD = Invoke < Task < Entity ? >> ($"{action}Ex", [entity]);
        if (methodCRUD == null)
        {
            return null;
        }

        var entityProcessed = await methodCRUD;
        if (entityProcessed == null)
        {
            SetLastError($"Failed to {action.ToString().ToLower()} {entity}!");
        }

        return entityProcessed;
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

    private DbSet<Entity>? GetDbSet()
    {
        foreach (var property in context.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                property.PropertyType.GetGenericArguments()[0] == typeof(Entity))
            {
                return (DbSet<Entity>?)property.GetValue(context);
            }
        }

        return null;
    }

    protected async Task<Entity?> CreateEx(Entity? entity)
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

        var dbSet = GetDbSet();
        if (dbSet == null)
        {
            return null;
        }

        propertyID.SetValue(entity, await dbSet.CountAsync() + 1);
        await dbSet.AddAsync(entity);
        return await context.SaveChangesAsync() > 0 ? entity : null;
    }

    protected async Task<Entity?> ReadEx(params object?[]? keyValues)

        => await context.FindAsync(typeof(Entity), keyValues) as Entity;

    /// <summary>
    /// Finds the entity by it's id and updates all it's properties
    /// </summary>
    /// <returns>true even if no properties changed</returns>
    protected async Task<Entity?> UpdateEx(Entity? entity)
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

        var entityUpdated = await ReadEx(propertyIDValue);
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
        return updated ? entityUpdated : null;
    }

    protected async Task<Entity?> DeleteEx(Entity? entity)
    {
        if (entity == null)
        {
            return null;
        }

        var dbSet = GetDbSet();
        if (dbSet == null)
        {
            return null;
        }

        dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0 ? entity : null;
    }

    private async Task<bool> Exists(Entity? entity)
    {
        if (entity == null)
        {
            return false;
        }

        var propertyID = entity.GetType().GetProperty("Id");
        if (propertyID == null)
        {
            return false;
        }

        var propertyIDValue = propertyID.GetValue(entity);
        if (propertyIDValue == null)
        {
            return false;
        }

        return await ReadEx(propertyIDValue) != null;
    }
}
}
