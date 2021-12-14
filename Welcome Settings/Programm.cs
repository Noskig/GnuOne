// See https://aka.ms/new-console-template for more information
using Library;
using Library.HelpClasses;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Welcome_Settings;

// Lägga connectionstringen Sparad i config/nått json

/// <summary>
/// skriv inlogg
/// skapas connectionsträng
/// kontakt db - om den inte finns, skapas
/// 
/// ladda frontend - skriva in mail - mailen sparas i databasen
/// läsa mail?
/// 
/// 
/// </summary>
///     
///ConnectionString blir fel när den serial eller deserialsizas...
///Skapa databas i detta program
///minimal API på Reactdelen
///
///Ta häjlp av frontend för att lägga en liten grund i denna app
///
/// 
/// 
/// ---------------------------------------------------------------
/// GJORT:
/// Ändra klass namn SqlScript till Global (klassen innehåller nu sqlscript + sätter connectionstringen)
/// Implementerat evighetsLoop med mailreader vart 5 sekund.
/// Implementerat AesCryption i HelpClass.
/// 
/// FRE
///     Kollar connectionsträngen om inloggningen var korrekt
///       -Först mot heidi. Genom att försöka skicka in bös. Det går = den finns
///       -Annars skapar med scriptet
///     Lagt allt i en while loop som snurrar. Glömt varför, men det känns gött xD
///     Gjort om strukturen lite, förhoppningsvist mer läsbar
/// 
///------------------------------------------------
/// 


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
                ID =1,
                Email = email,
                Password = pw,
                Username = username,
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
}




static void WriteToJson(string sectionPathKey, string value)
{
    string path = @"..\GnuOne\appsettings.json";
    string fullpath = Path.GetFullPath(path);
    Console.WriteLine(Path.GetFullPath(path));
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

