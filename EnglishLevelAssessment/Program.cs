
using EnglishLevelAssessment.Components;
using EnglishLevelAssessment.Data.Models;
using EnglishLevelAssessment.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EnglishLevelAssessment");

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAntiforgery();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/login";
    });
builder.Services.AddAuthorization();

builder.Services.AddBlazorBootstrap();
builder.Services.AddDbContextFactory<EnglishLevelAssessmentContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<MaturaService>();
builder.Services.AddScoped<CollegeService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<AnswerService>();
builder.Services.AddScoped<LanguageLevelService>();
builder.Services.AddScoped<ResultService>();
builder.Services.AddScoped<UserService>();
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

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
