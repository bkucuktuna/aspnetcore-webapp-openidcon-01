using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRazorPages().AddRazorPagesOptions
// Add services to the container.
// builder.Services.AddRazorPages().AddMvcOptions(options =>
// {
//     var policy=new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//     options.Filters.Add(new AuthorizeFilter(policy));
// }
// ).AddMicrosoftIdentityUI();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAD"));

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy=options.DefaultPolicy;
    });
builder.Services.AddRazorPages().AddMicrosoftIdentityUI();
//builder.Services.AddMicrosoftIdentityWebAppAuthentication(configuration: builder.Configuration.AddJsonFile("appSettings.json").Build(),configSectionName: "AzureAD");

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
