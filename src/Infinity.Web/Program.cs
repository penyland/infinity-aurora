using Infinity.Web.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddLoggingServices();

    //builder.AddAuthenticationServices();
    //builder.AddAzureServices("Infinity.Web");
    builder.AddFeatureManagementServices();

    builder.Services.AddRazorPages(options => options.RootDirectory = "/Features");
    builder.Services.AddServerSideBlazor()
        .AddMicrosoftIdentityConsentHandler();

    builder.Services.AddControllersWithViews()
        .AddMicrosoftIdentityUI();

    // builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

    builder.Services.AddBlazorStrap();

#if DEBUG
    builder.Services.AddSassCompiler();
#endif

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.ConsoleTheme.None)
            .CreateLogger();
    }

    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shut down complete.");
    Log.CloseAndFlush();
}
