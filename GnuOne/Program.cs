using GnuOne.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Welcome_Settings;

/// <summary>
/// 
/// </summary>



//När vi kör welcome och startar gnuone.exe. Funkar inta kontakten med databasen. Det funkar om connectionstringen är hårdkordad.
/// Vill vi kanske att GnuOne startar Welcome settings/mail loopen. istället för tvärt om?
/// 
/// Mer att göra. Göra klart contoller.
/// Frontend - lägga in sin app i denna.

//async Mailloop();
//Process process = new Process();


//// Configure the process using the StartInfo properties.
//process.StartInfo.FileName = "process.exe";
//process.StartInfo.Arguments = "-n";
//process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
//process.Start();
//process.WaitForExit();// Waits here for the process to exit.


var builder = WebApplication.CreateBuilder(args);

string _connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApiContext>(Options =>
            Options
                .UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring))
            );

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            ///för att tillåta kommunikation från/till FrontEND
            builder.AllowAnyHeader();
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();

            string[] array = { "https://localhost:5001", "http://localhost:5000", "http://localhost:7261", "http://localhost:5261" };
            builder.WithOrigins(array);
        });
});
builder.Services.AddControllersWithViews();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();


/// Flytta över controllers hit med mailfunktionen
///     AddDbcontext.
///         Skriva in connectionsträngen in i Json fil. från Welcome settings. Använda appsettings för att skapa dbcontexten
///        
/// 
/// Frågor. Va händer när man göra en Process.Start från Welcomesettings.