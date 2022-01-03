using GnuOne.Data;
using Library;
using Library.HelpClasses;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using Welcome_Settings;

/// <summary>
/// 
/// </summary>
bool oo = true;
while (oo)
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
            oo = false;
        }
        else
        {
            ///kanske skicka till frontend för att fylla i.

            Console.Clear();
            Console.Write("Write your Email: ");
            var email = Console.ReadLine();
            Console.Write("EmailPassword: ");
            var pw = Console.ReadLine();
            Console.Write("choose your username: ");
            var username = Console.ReadLine();

            var settings = new MySettings
            {
                ID = 1,
                Email = email,
                Password = pw,
                userName = username,
                Secret = "secretkey"
            };
            try
            {

                await DbContext.MySettings.AddRangeAsync(settings);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            oo = false;

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
var loop1Task = Task.Run(async () => {
    while (true)
    {
        using (MariaContext context = new MariaContext(_connectionstring))
        {
            MailReader.ReadUnOpenEmails(context, _connectionstring);
            await Task.Delay(5000);
            a++;
            Console.Write(a);
        }
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

    var inputU = Console.ReadLine();

    Console.Write("Password: ");

    string inputP = pwMask.pwMasker();

    Console.WriteLine();

    ///connectionstringen byggs
    string newConn = "server=localhost;user id=" + inputU + ";password=" + inputP + ";";
    /// den fullständiga med DB till global
    Global.CompleteConnectionString = "server=localhost;user id=" + inputU + ";password=" + inputP + ";database=gnu;";

    ///write to appsettings.json



    return newConn;
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
static void SetValueRecursively(string sectionPathKey, dynamic? jsonObj, string value)
{
    // split the string at the first ':' character
    var remainingSections = sectionPathKey.Split(":", 2);

    var currentSection = remainingSections[0];
    if (remainingSections.Length > 1)
    {
        // continue with the procress, moving down the tree
        var nextSection = remainingSections[1];
        SetValueRecursively(nextSection, jsonObj[currentSection], value);
    }
    else
    {
        // we've got to the end of the tree, set the value
        jsonObj[currentSection] = value;
    }
}