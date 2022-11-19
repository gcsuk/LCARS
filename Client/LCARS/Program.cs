using LCARS.Data;
using Refit;
using LCARS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();
builder.Services.AddScoped<AlertState>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<PullRequestService>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<BuildsService>();
builder.Services.AddScoped<DeploymentsService>();

var baseUrl = builder.Configuration["Api:BaseUrl"];

builder.Services.AddRefitClient<IApiClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRequestLocalization("en-GB");

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();