// See https://aka.ms/new-console-template for more information
using Library.HelpClasses;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Welcome_Settings;

// Lägga connectionstringen Sparad i config/nått json

/// <summary>
/// skriv inlogg
/// 
/// skapas connectionsträng
/// 
/// kontakt db - om den inte finns, skapas
/// 
/// ladda frontend - skriva in mail - mailen sparas i databasen
/// 
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



/// new global.connectstring = connectionstring;

var connectionstring = EnterCredentials(); ///Behöver spara för att användas i andra delar av programmet

MariaContext context = new MariaContext(connectionstring);


CreateDatabase(CheckIfDatabaseExist(context), connectionstring);

CheckMailCredentials(context);

/// check if mailadress exist in db
/// if yes - start reading mail
/// if no - Send to frontend to enter and then save mailadress



static void CheckMailCredentials(MariaContext context)
{
    //finns det nått finns det allt - Frontend validering
    if (context.MySettings.Any())
    {
        ///starta maillop
        Console.WriteLine("mailloop startad");
    }
    else
    {
        Console.WriteLine("Skickar till frontend");
        ///skicka till frontend create mail - som pingar api?
    }

}
static void CreateDatabase(bool created, string connection)
{
    if (created)
    {
        Console.WriteLine("There is atleast some information in the database");
        return;
    }
    else
    {
        DbCommand.CreateCommand(ScriptSql.sql, connection);
        Console.WriteLine("Database was created");
    }

}
static bool CheckIfDatabaseExist(MariaContext context)
{


    if (context.LastUpdates.Any())
    {
        return true;
    }
    else return false;

}
static string EnterCredentials()
{
        Console.WriteLine("Hello! \n"
            + "Thanks for using this app. This first time.\n " +
            "Please enter your heidi-username and password.");
        Console.WriteLine();
        Console.Write("Username: ");

        var inputU = Console.ReadLine();

        Console.Write("Password: ");

        var inputP = Console.ReadLine();

        Console.WriteLine();

        string newConn = "server=localhost;user id=" + inputU + ";password=" + inputP + ";database=gnu;";

        return newConn;
        Console.WriteLine(newConn);
        Console.WriteLine("thanks");
}



