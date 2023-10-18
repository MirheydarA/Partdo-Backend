using Business.Services.Abstract.Admin;
using Business.Services.Abstract.Users;
using Business.Services.Concrete.Admin;
using Business.Services.Concrete.Users;
using Business.Services.Utilities.Abstract;
using Business.Services.Utilities.Concrete;
using Common.Entities;
using DataAccess.Contexts;
using DataAccess.DbInitializer;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.Repositories.Concrete.Users;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddSingleton<IFileService, FileService>();


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();



/////////////////////Admin Repositories/////////////////////
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Admin.ICategoryRepository, DataAccess.Repositories.Concrete.Admin.CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Admin.IProductRepository, DataAccess.Repositories.Concrete.Admin.ProductRepository>();
builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IOnSale_1Repository, OnSale_1Repository>();
builder.Services.AddScoped<IOnSale_2Repository, OnSale_2Repository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

///////////////////////////////////////////////////////////


/////////////////////Users Repositories/////////////////////
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Users.IProductRepository, DataAccess.Repositories.Concrete.Users.ProductRepository>();
builder.Services.AddScoped<DataAccess.Repositories.Abstract.Users.ICategoryRepository, DataAccess.Repositories.Concrete.Users.CategoryRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
////////////////////////////////////////////////////////


/////////////////////Admin Services/////////////////////
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IBlogService, Business.Services.Concrete.Admin.BlogService>();
builder.Services.AddScoped<IOnSale_1Service, OnSale_1Service>();
builder.Services.AddScoped<IOnSale_2Service, OnSale_2Service>();
builder.Services.AddScoped<Business.Services.Abstract.Admin.IAccountService, Business.Services.Concrete.Admin.AccountService>();
////////////////////////////////////////////////////////



/////////////////////Users Services/////////////////////
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<Business.Services.Abstract.Users.IBlogService, Business.Services.Concrete.Users.BlogService>();
builder.Services.AddScoped<Business.Services.Abstract.Users.IAccountService, Business.Services.Concrete.Users.AccountService>();
builder.Services.AddScoped<IBasketService, BasketService>();
////////////////////////////////////////////////////////


builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUnitOfWork,  UnitOfWork>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.HttpContext.Request.Path.Value.StartsWith("/admin") || context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/admin/account/login" + redirectPath.Query);
        }
        else
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/account/login" + redirectPath.Query);
        }
        return Task.CompletedTask;
    };

});


var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    await DbInitializer.SeedAsync( roleManager, userManager );
}


app.Run();
