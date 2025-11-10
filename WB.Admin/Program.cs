using AutoMapper;
using BlazorColorPicker;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging.Abstractions;
using Radzen;
using Soenneker.Blazor.FilePond.Registrars;
using Soenneker.Blazor.TomSelect.Registrars;
using WB.Admin.Extensions;
using WB.Admin.Helpers;
using WB.Admin.Mappings;
using WB.Admin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddColorPicker();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddFilePondInteropAsScoped();
builder.Services.AddTomSelectInteropAsScoped();
builder.Services.AddScoped<StateService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<IActionService, ActionService>();
builder.Services.AddWMBOS();
builder.Services.AddWMBSC();
builder.Services.AddScoped<MenuDataService>();
builder.Services.AddScoped<NavScrollService>();
builder.Services.AddSession();
builder.Services.AddScoped<SessionService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<TranslationState>();
builder.Services.AddSingleton<SystemSettingState>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<ApiClientService>();
builder.Services.AddScoped<LoginState>();
builder.Services.AddScoped<InvalidRequestHandlerService>();
builder.Services.AddSingleton<GridSearchExtension>();
builder.Services.AddBlazorDownloadFile();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddServerSideBlazor().AddHubOptions(o => { o.MaximumReceiveMessageSize = 102400000; });
builder.Services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });

#region Http Clients
builder.Services.AddHttpClient("WB", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"));
});
#endregion
// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
        options.Cookie.Name = "YourAppCookieName";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

#region Authentication State

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();

#endregion

#region Auto Mapper
var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new ClientMappingProfile());
}, new NullLoggerFactory());
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
    
app.UseAuthentication();
app.UseAuthorization();

app.MapFallbackToPage("/_Host");

app.Run();


