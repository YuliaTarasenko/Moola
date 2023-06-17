using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Moola;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //add database
        builder.Services.AddDbContext<MyContext>(b=>b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
            .AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
            .Services.AddSwaggerGen();

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

        app.Run();
    }
}