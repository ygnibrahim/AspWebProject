using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure options and memory cache
builder.Services.AddOptions();
builder.Services.AddMemoryCache();

// Configure IP Rate Limiting
builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));

// Add IP rate limiting services
builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

var app = builder.Build();

// Seed IP policies
using (var scope = app.Services.CreateScope())
{
    var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();
    ipPolicyStore.SeedAsync().Wait();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting(); // Use IP rate limiting middleware

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
