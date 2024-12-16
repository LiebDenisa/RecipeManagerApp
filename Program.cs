using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Configure the database context
builder.Services.AddDbContext<RecipeManagerAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecipeManagerAppContext") ??
        throw new InvalidOperationException("Connection string 'RecipeManagerAppContext' not found.")));

// 2. Configure Identity with role management
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Enables role management
    .AddEntityFrameworkStores<RecipeManagerAppContext>();

// 3. Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication is enabled
app.UseAuthorization();

app.MapRazorPages();

app.Run();
