using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Library.Models;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using Welcome_Settings;

/// <summary>
/// 
/// 
/// PUBLISHA - kunna köra utan VS
/// 
///  
/// 
/// Lägga till vänner / grupper -- Hur blir det med nycklar. 
///     Bjuda in via mail? - vanlig mail / dedikerad gmail?
///     Krypera lösenord till mailen, nycklar mellan sina vänner/ grupper.
///     PM
/// 
/// --bittorrent--
/// backup - restore
/// 
/// </summary>
bool keepGoing = true;
while (keepGoing)
{
    Global.ConnectionString = EnterCredentials();
    MariaContext context = new MariaContext(Global.ConnectionString);
    MariaContext DbContext = new MariaContext(Global.CompleteConnectionString);
    WriteToJson("ConnectionStrings:Defaultconnection", Global.CompleteConnectionString);

    //kollar om det är rätt inlog med att skicka någonting till db:n
    if (await CheckConnection(context))
    {
        //no gnu - skapa db
        if (!IsThereAGnu(DbContext))
        {
            CreateDatabase(Global.ConnectionString);
        }

        //finns det i db.mysettings
        if (IsThereMailCredentials(DbContext))
        {
            keepGoing = false;
        }
        else
        {
            ///kanske skicka till frontend för att fylla i.

            Console.Clear();
            Console.Write("Write your Email: ");
            var email = Console.ReadLine();
            Console.Write("EmailPassword: ");
            var password = Console.ReadLine();
            Console.Write("choose your username: ");
            var username = Console.ReadLine();

            var settings = new MySettings
            {
                ID = 1,
                Email = email,
                Password = password,
                userName = username,
                Secret = "secretkey"
            };
            var profile = new myProfile
            {
                ID = 1,
                Email = email,
                pictureID = 1,
                
            };
            try
            {
                await DbContext.MyProfile.AddAsync(profile);
                await DbContext.MySettings.AddAsync(settings);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            keepGoing = false;

        };
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Det gick inte att ansluta till databasen testa igen");
        Console.WriteLine();
    }

}

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
            ///får att tillåta kommunikation från/till FrontEND
            builder.AllowAnyHeader();
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();

            string[] array = { "https://localhost:5001", "http://localhost:5000", "http://localhost:7261", "http://localhost:5261", "https://localhost:44486", "https://localhost:7261" };
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
app.MapFallbackToFile("index.html");


int a = 0; //Visualiserar att mailfunktionen rullar.
var loop1Task = Task.Run(async () =>
{
    while (true)
    {
        try
        {
            using (MariaContext context = new MariaContext(_connectionstring))
            {
                MailReader.ReadUnOpenEmails(context, _connectionstring);
                a++;
                Console.Write(a);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    await Task.Delay(10000);
    }
});
app.Run();


bool IsThereAGnu(MariaContext dbcontext)
{
    try
    {
        dbcontext.LastUpdates.Any();
        return true;
    }
    catch
    {
        return false;
    }
}
static bool IsThereMailCredentials(MariaContext _newContext)
{
    //finns det nått finns det allt - Frontend validering
    if (_newContext.MySettings.Any())
    {
        return true;
    }
    else
    {
        return false;
    }
}
static void CreateDatabase(/*bool created,*/ string connection)
{
    DbCommand.CreateCommand(Global.sql, connection);
}
static async Task<bool> CheckConnection(MariaContext context)
{
    try
    {   //Försöker skicka in ett vanligt sql scrip
        var dontreallycareaboutwhatgetsback = await context.Database.ExecuteSqlRawAsync("SELECT 1");
    }
    catch (Exception)
    {
        return false;
    }
    return true;
}
static string EnterCredentials()
{
    Console.WriteLine("Hello! \n"
                      + "Thanks for using this app.\n" +
                          "Please enter your heidi-username and password.");
    Console.WriteLine();
    Console.Write("Username: ");

    var inputUserName = Console.ReadLine();

    Console.Write("Password: ");

    string inputPassWord = pwMask.pwMasker();

    Console.WriteLine();

    ///connectionstringen byggs
    string newConnection = "server=localhost;user id=" + inputUserName + ";password=" + inputPassWord + ";";
    /// den fullständiga med DB till global
    Global.CompleteConnectionString = "server=localhost;user id=" + inputUserName + ";password=" + inputPassWord + ";database=gnu;";

    ///write to appsettings.json

    return newConnection;
    //Console.Clear();
    //Console.ForegroundColor = ConsoleColor.Blue;
    //Console.WriteLine(" - Dont shut this window down - ");
    //Console.WriteLine();
}
static void WriteToJson(string sectionPathKey, string value)
{
    string file = "\\appsettings.json";
    string path = Directory.GetCurrentDirectory();
    string fullpath = Path.GetFullPath(path + file);

    //Console.WriteLine(Path.GetFullPath(path));
    try
    {
        var filePath = fullpath;
        string json = File.ReadAllText(filePath);
        dynamic? jsonObj = JsonConvert.DeserializeObject(json);

        SetValueRecursively(sectionPathKey, jsonObj, value);

        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, output);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error writing app settings | {0}", ex.Message);
    }
}
static void SetValueRecursively(string sectionPathKey, dynamic? jsonObject, string value)
{
    // split the string at the first ':' character
    var remainingSections = sectionPathKey.Split(":", 2);

    var currentSection = remainingSections[0];
    if (remainingSections.Length > 1)
    {
        // continue with the procress, moving down the tree
        var nextSection = remainingSections[1];
        SetValueRecursively(nextSection, jsonObject[currentSection], value);
    }
    else
    {
        // we've got to the end of the tree, set the value
        jsonObject[currentSection] = value;
    }
}