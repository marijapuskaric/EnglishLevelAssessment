using EnglishLevelAssessment.Authentication;
using EnglishLevelAssessment.Components;
using EnglishLevelAssessment.Data.Models;
using EnglishLevelAssessment.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EnglishLevelAssessment");

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddMudServices();
builder.Services.AddDbContext<EnglishLevelAssessmentContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<MaturaService, MaturaService>();
builder.Services.AddScoped<CollegeService, CollegeService>();
builder.Services.AddScoped<QuestionService, QuestionService>();
builder.Services.AddScoped<AnswerService, AnswerService>();
builder.Services.AddScoped<LanguageLevelService, LanguageLevelService>();
builder.Services.AddScoped<ResultService, ResultService>();
builder.Services.AddScoped<UserService, UserService>();
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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
