using LionPetManagement_BLL;
using LionPetManagement_DAO;
using LionPetManagement_DAO.Basic;
using LionPetManagement_HoaLQ.Hub;
using LionPetManagement_HoaLQ.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SU25LionDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<LionAccountService>(); 
builder.Services.AddScoped<LionAccountRepository>();
builder.Services.AddScoped<LionProfileService>();
builder.Services.AddScoped<LionProfileRepository>();
builder.Services.AddScoped<LionTypeService>();
builder.Services.AddScoped<LionTypeRepository>();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Account/Forbidden";
        options.LoginPath = "/Login";
        options.LogoutPath = "/Account/Logout";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<LionProfileHub>("/LionProfileHub");

app.MapRazorPages().RequireAuthorization();

app.Run();
