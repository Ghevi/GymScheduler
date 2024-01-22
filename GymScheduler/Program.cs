using GymScheduler.Components;
using GymScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using webenology.blazor.components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<AppDbContext>(o
    => o.UseSqlServer(builder.Configuration.GetConnectionString("GymScheduler")) );
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<AppDbContext>());
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddWebenologyHelpers();
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

using (var dbc = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext())
{
    dbc.Database.Migrate();
}

app.Run();
