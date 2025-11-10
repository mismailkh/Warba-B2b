using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WB.API.Helpers;
using WB.Application.Interfaces.Repositories;
using WB.Application.Interfaces.Services;
using WB.Application.Mappings;
using WB.Application.Services;
using WB.Domain.Entities.Lookups;
using WB.Domain.Entities.Ums;
using WB.Infrastructure.DbContext;
using WB.Infrastructure.Repository;
using WB.Shared.Dtos.UMS.Others;
using Microsoft.AspNetCore.Identity;
using WB.Application.SignalR;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<User>), typeof(GenericRepository<User>));
builder.Services.AddScoped(typeof(IGenericRepository<UserRoles>), typeof(GenericRepository<UserRoles>));
builder.Services.AddScoped(typeof(IGenericRepository<Claim>), typeof(GenericRepository<Claim>));
builder.Services.AddScoped(typeof(IGenericRepository<GroupClaims>), typeof(GenericRepository<GroupClaims>));
builder.Services.AddScoped(typeof(IGenericRepository<Translation>), typeof(GenericRepository<Translation>));
builder.Services.AddScoped(typeof(IGenericRepository<Language>), typeof(GenericRepository<Language>));
builder.Services.AddScoped(typeof(IGenericRepository<UserPersonalInformation>), typeof(GenericRepository<UserPersonalInformation>));
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICarMakeService, CarMakeService>();
builder.Services.AddScoped<ICarMakeRepository, CarMakeRepository>();
builder.Services.AddScoped<IProductPackageService, ProductPackageService>();
builder.Services.AddScoped<IProductPackageRepository, ProductPackageRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new MappingProfile());
}, new NullLoggerFactory());
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Authentication
var jwtsettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtsettings), jwtsettings);
builder.Services.AddSingleton(jwtsettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Default to JwtBearer'
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = "warba",
        ValidAudience = "warba",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:Secret")))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var authHeaders = context.Request.Headers["Authorization"];
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(authHeaders) && path.StartsWithSegments("/notifications"))
            {
                context.Token = authHeaders.ToString().Substring("Bearer ".Length).Trim();
            }
            return Task.CompletedTask;
        }
    };
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHelper>("WBApiKey", options => { });
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationsHub>("notifications");

app.Run();
