using blazorpg.Components;
using blazorpg.Data.Services;
using blazorpg.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddRazorData.Services()
//     .AddInteractiveServerComponents();

builder.Services.AddRazorComponents()         //aaaaaaaaa
    .AddInteractiveServerComponents();

builder.Services.AddScoped<CharacterService>(); 
builder.Services.AddScoped<CharacterTypeService>();
builder.Services.AddScoped<EnemyService>();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<LocalStorageAccessor>(); uu

builder.Services.AddScoped<WeatherForecastService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
