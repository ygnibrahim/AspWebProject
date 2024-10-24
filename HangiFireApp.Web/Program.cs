using Hangfire; // Bu sat�r �nemli!
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Hangfire'� SQL Server ile kullanmak i�in connection string'i al�yoruz.
string hangfireConnectionString = builder.Configuration.GetConnectionString("HangFireConnection");

// Hangfire'� SQL Server ile konfig�re ediyoruz
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseSqlServerStorage(hangfireConnectionString, new Hangfire.SqlServer.SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           });
});

// Hangfire server ve dashboard'u ekliyoruz
builder.Services.AddHangfireServer();
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Hangfire dashboard'u ekliyoruz
app.UseHangfireDashboard(); // Hata al�nmamas� i�in Hangfire namespace'i eklendi

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
