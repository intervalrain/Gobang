using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MudBlazor.Services;
using Server;
using Server.Services;
using Server.Hubs;
using Server.DataModels;
using Server.Presenters;
using Application;
using Application.Usecases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR()
    .AddJsonProtocol();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddGobangServer();
builder.Services.AddGobangApplication();
//builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",
//    configurePolicy =>
//    {
//        configurePolicy.AllowAnyHeader()
//                       .AllowAnyMethod()
//                       .SetIsOriginAllowed(_ => true)
//                       .AllowCredentials();
//    }));

//builder.WebHost.UseUrls(new[] { "http://*:2451" });


// Configure the HTTP request pipeline.
//if (builder.Environment.IsDevelopment())
//{
//    builder.Services.AddScoped<IPlatformService, DevelopmentPlatformService>();
//}
//else
//{
//    builder.Services.AddScoped<IPlatformService, PlatformService>();
//}
//builder.Services.AddSingleton<PlatformJwtEvents>();
//builder.Services
//    .AddOptions<JwtBearerOptions>("Bearer")
//    .Configure<PlatformJwtEvents>((opt, jwtEvents) =>
//    {
//        builder.Configuration.Bind(nameof(JwtBearerOptions), opt);
//        opt.Events = jwtEvents;
//    });
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseCors("CorsPolicy");
//app.UseAuthentication();
//app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<BlazorChatSampleHub>(BlazorChatSampleHub.HubUrl);
app.MapHub<GobangHub>(GobangHub.HubUrl);

app.Run();
