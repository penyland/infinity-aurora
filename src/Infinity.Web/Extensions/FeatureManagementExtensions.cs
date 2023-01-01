using Microsoft.FeatureManagement;

namespace Infinity.Web.Extensions;

public static class FeatureManagementExtensions
{
    public static IFeatureManagementBuilder AddFeatureManagementServices(this WebApplicationBuilder builder) =>
        builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement:Web"));
}
