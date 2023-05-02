using LCARS.Endpoints;
using LCARS.Middleware;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpoints(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseEndpoints();

app.UseHealthChecks("/ping");

app.Run();