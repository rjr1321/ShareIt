using Microsoft.AspNetCore.Identity;
using ShareIt.Core.Application;
using ShareIt.Infrastructure.Identity;
using ShareIt.Infrastructure.Identity.Seeds;
using ShareIt.Infrastructure.Shared;
using ShareIt.Infrastructure.Persistence;
using ShareItApp.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructurePersistenceLayer(builder.Configuration);
builder.Services.AddInfrastructureSharedLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureIdentityLayer(builder.Configuration);

builder.Services.AddScoped<LoginAuth>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();



        await DefaultRoles.SeedAsync(userManager, roleManager);
      /*  await DefaultAdmin.SeedAsync(userManager, roleManager);
        await DefaultUser.SeedAsync(userManager, roleManager);*/
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Publication}/{action=Index}/{id?}");

app.Run();
