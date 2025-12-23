using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentProjects.API.Middleware.Documentation;

/// <summary>
/// Applies all response types api conventions
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    public static void AddApiConventionResponseTypes(this IEndpointConventionBuilder builder)
    {
        builder.Add(endpointBuilder =>
        {
            var action = endpointBuilder.Metadata.OfType<ControllerActionDescriptor>().FirstOrDefault();
            if (action is not null)
            {
                var explicitResponseTypes = endpointBuilder.Metadata.OfType<ProducesResponseTypeAttribute>().ToList();
                if (explicitResponseTypes.Count > 0)
                {
                    if (action.Properties.TryGetValue(typeof(ApiConventionResult), out var result) && result is ApiConventionResult apiConventionResult)
                    {
                        foreach (var response in apiConventionResult.ResponseMetadataProviders)
                        {
                            action.EndpointMetadata.Add(response);
                            action.FilterDescriptors.Add(new FilterDescriptor(response, 30));
                        }
                    }
                }
            }
        });
    }
}