using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ProjectManagementAPl.ViewModels;
using Repositories;
using System.Text;
namespace ProjectManagementAPl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program));


            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntityType<Product>();
            modelBuilder.EntityType<ProductModel>();
            modelBuilder.EntityType<Category>();
            modelBuilder.EntityType<ViewProduct>();
            modelBuilder.EntityType<Order>();
            modelBuilder.EntityType<OrdersDetail>();
            modelBuilder.EntityType<RoleAccount>();
            modelBuilder.EntityType<Customer>();
            //modelBuilder.EntitySet<Product>("Products");
            modelBuilder.EntitySet<ProductModel>("Products").EntityType.Name = "Products";
            modelBuilder.EntitySet<Category>("Categorys");
            //modelBuilder.EntitySet<Customer>("Customers");

            builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand().SetMaxTop(100)
                .AddRouteComponents("odata", modelBuilder.GetEdmModel()));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //config JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            //---------

            // Add Repository
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            


            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"));
            });

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseODataBatching();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            //Configure ajax
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();




            app.Run();
        }
    }
}