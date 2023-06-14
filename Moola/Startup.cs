using Microsoft.AspNetCore.Authentication.Cookies;

namespace Moola;
public class Startup
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //add database
        builder.Services.AddDbContext<MyContext>();
        //add authentication
        builder.Services.AddAuthentication(options => options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(c=>c.LoginPath = "/Users/Login");
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

       // builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

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