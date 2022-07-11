using LCARS.Endpoints.Internal;
using LCARS.Services.ApiClients;
using Refit;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRefitClient<IGitHubClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["GitHub:BaseUrl"]));

builder.Services.AddEndpoints<Program>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseEndpoints<Program>();

app.Run();