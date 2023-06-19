using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Moola.Models;
using System;

namespace Moola;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //add database
        builder.Services.AddDbContext<MyContext>(b => b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        //add authentication
        builder.Services.AddAuthentication(options => options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        // Add services to the container.
        builder.Services.AddControllersWithViews()
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
                options.HtmlHelperOptions.Html5DateRenderingMode = Microsoft.AspNetCore.Mvc.Rendering.Html5DateRenderingMode.CurrentCulture;
            })
            .AddDataAnnotationsLocalization()
            .AddMvcLocalization()
            .Services
            .AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

        //add swagger
        builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

        //add swagger
        app.UseSwagger();
        //add swagger ui
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Moola API V1");
        });

        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //user authentication and authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        ConfigureAPIs(app);
        app.Run();
    }


    public static void ConfigureAPIs(IEndpointRouteBuilder application)
    {
        application.MapGet("/incomes", (MyContext db) => db.Incomes.ToList());
        application.MapGet("/incomes/{id:int}", (int id, MyContext db) =>
        {
            var income = db.Incomes.Find(id);
            return income == null ? Results.NotFound() : Results.Ok(income);
        });
        application.MapPost("/incomes", (IncomeDto income, MyContext db) =>
        {
            if (db.Incomes.Any(i => i.Id == income.Id))
                return Results.BadRequest("Income already exists");
            var dbIncome = new Income(income.Id, income.IncomeDate, income.Amount, income.CategoryId, income.Note, income.FinanceId);
            db.Incomes.Add(dbIncome);
            db.SaveChanges();
            return Results.Created($"/incomes/{dbIncome.Id}", dbIncome);
        });
        application.MapPut("/incomes", (IncomeDto income, MyContext db) =>
        {
            if (!db.Incomes.Any(i => i.Id == income.Id))
                return Results.BadRequest("Income is not updateable, because it not exists");
            var dbIncome = db.Incomes.Find(income.Id);
            db.Entry(dbIncome!).CurrentValues.SetValues(income);
            db.SaveChanges();
            return Results.Created($"/incomes/{dbIncome.Id}", dbIncome);
        });
        application.MapDelete("/incomes", (int id, MyContext db) =>
        {
            var income = db.Incomes.Find(id);
            if (income == null) return Results.NotFound("No income found");
            db.Incomes.Remove(income);
            db.SaveChanges();
            return Results.Ok("Income deleted");
        });
    }
}