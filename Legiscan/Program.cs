using Legiscan.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<GraphController>();
builder.Services.AddSingleton<PersonController>();

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTg4MDkyM0AzMjMxMmUzMTJlMzMzNUg3TjN6T1JBNzlsYS96UVNzOXNuUXZJdUlycklMcGZaWFlycWV5Z3VqR2M9;Mgo+DSMBaFt+QHFqVkNrWU5DaV1CX2BZfVl1RGlcfE4QCV5EYF5SRHJcQlxmSn9WdUBqXH8=;Mgo+DSMBMAY9C3t2VFhhQlJBfVpdWnxLflF1VWBTfF96cVZWACFaRnZdQV1nSn5Sc0ZiXHxYcHJV;Mgo+DSMBPh8sVXJ1S0d+X1RPc0BFQmFJfFBmRmlaflR1d0UmHVdTRHRcQllgSX9WdURmWnlfeHQ=;MTg4MDkyN0AzMjMxMmUzMTJlMzMzNUVIOUxCdTFDTkF2dE4rNWwwUng0MnNGVkF6WjBDMXBWU0o0RG5MbmwycDg9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RHQmRWfFN0RnNadV1xflBFcDwsT3RfQF5jTX1Td0BgWHpddnBcRg==;ORg4AjUWIQA/Gnt2VFhhQlJBfVpdWnxLflF1VWBTfF96cVZWACFaRnZdQV1nSn5Sc0ZiXHxYdnVV;MTg4MDkzMEAzMjMxMmUzMTJlMzMzNVNISlI3RUdPY2Zzd2FEaXV3Y2QxekpWNkkxOC9CTG5CRVRpdW9ua2VnT1E9;MTg4MDkzMUAzMjMxMmUzMTJlMzMzNUlFUXVtb2pPcVBsdEhiNXdHaUw3YXcybTkzY2lyKzlNZFlrdXFoUW1qYzg9;MTg4MDkzMkAzMjMxMmUzMTJlMzMzNUR5RHM2VUVBT2JYeFJObGFzTlRXSjNKMTdWVHdULzZRbE1rWTc2S1doM1E9;MTg4MDkzM0AzMjMxMmUzMTJlMzMzNVgxYjdncjMzbEhldWNlcVJScEF5SlhMYTlZMlB1M0ZKemN1WHJsTUo0L2s9;MTg4MDkzNEAzMjMxMmUzMTJlMzMzNWtGYWFlTnVXUjZLeWd4Q0ZuNjJHYVBZN2JGWUJiZWdCVTdqc0pMMDkwNEU9");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
