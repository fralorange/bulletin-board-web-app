using BulletinBoard.Contracts.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BulletinBoard.Contracts.Filters
{
    /// <summary>
    /// Фильтр для Сваггера.
    /// </summary>
    public class BuggedOperationFilter : IOperationFilter
    {
        /// <inheritdoc cref="IOperationFilter"/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var buggedAttribute = context.MethodInfo.GetCustomAttribute<BuggedAttribute>();
            if (buggedAttribute != null)
            {
                operation.Description = $"BUGGED: {buggedAttribute.Message}";
            }
        }
    }
}
