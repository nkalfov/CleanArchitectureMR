using CleanArchitecture.Presentation.Configuration.ServiceCollection;
using CleanArchitecture.Presentation.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.AddIOptions();
builder.AddCommon();
builder.AddPersistance();
builder.AddInfrastructure();
builder.AddApplication();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler(CommonRoutes.Error);
app.UseStatusCodePagesWithRedirects(CommonRoutes.NotFound);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();