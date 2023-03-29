using CleanArchitecture.Presentation.Configuration.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

builder.AddIOptions();
builder.AddCommon();
builder.AddPersistance();
builder.AddInfrastructure();
builder.AddApplication();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();  
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();