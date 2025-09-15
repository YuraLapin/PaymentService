using Mediator;
using Microsoft.EntityFrameworkCore;
using PaymentService.DataAccess.Postgres;
using PaymentService.WebApi.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

string connString = builder.Configuration["ConnectionStrings:Postgres"];

// Add services to the container.
builder.Services.AddSingleton<InputChecker>();
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connString));
builder.Services.AddControllersWithViews();
builder.Services.AddMediator((MediatorOptions options) =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Payments}/{action}/{id?}");

app.Run();
