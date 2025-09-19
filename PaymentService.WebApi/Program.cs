using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;
using PaymentService.DataAccess.Postgres;
using PaymentService.WebApi.Services;
using PaymentService.WebApi.Validators;

namespace PaymentService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            string connString = builder.Configuration["ConnectionStrings:Postgres"];

            builder.Services.AddSingleton<ProducerService>();
            builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connString));
            builder.Services.AddControllersWithViews();
            builder.Services.AddValidatorsFromAssemblyContaining<PaymentValidator>();
            builder.Services.AddMediator((MediatorOptions options) =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Payments}/{action}/{id?}");

            app.Run();
        }
    }
}
