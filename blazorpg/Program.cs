using blazorpg.Components;
using blazorpg.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddRazorData.Services()
//     .AddInteractiveServerComponents();

builder.Services.AddRazorComponents()         //aaaaaaaaa
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CharacterService>(); 
builder.Services.AddSingleton<WeatherForecastService>(); 

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
