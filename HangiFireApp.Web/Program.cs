using Hangfire; // Bu satýr önemli!
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Hangfire'ý SQL Server ile kullanmak için connection string'i alýyoruz.
string hangfireConnectionString = builder.Configuration.GetConnectionString("HangFireConnection");

// Hangfire'ý SQL Server ile konfigüre ediyoruz
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
app.UseHangfireDashboard(); // Hata alýnmamasý için Hangfire namespace'i eklendi

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
