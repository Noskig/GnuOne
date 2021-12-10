// See https://aka.ms/new-console-template for more information
using Library;
using Library.HelpClasses;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
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


bool oo =true;
while (oo) { 


    Global.ConnectionString = EnterCredentials();

    MariaContext context = new MariaContext(Global.ConnectionString);
    MariaContext DbContext = new MariaContext(Global.CompleteConnectionString);

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
            ///kanske behöver kolla om det är rätt uppgifter för email-log in. 
            ///Kanske om vi eller frontend skickar det att logga in via Gmail. Så att det måste bli rätt.?


            int i = 0;

            Console.WriteLine("Endless Mail-read LOOOP begins now.");
            while (true)
            {
              
                ReadUnOpenEmails(DbContext, Global.CompleteConnectionString);

                i++;
                Console.WriteLine($"Read email {i} times");

                Thread.Sleep(5000);
            }
        }
        else
        {
            ///skicka till frontend för att fylla i.
            throw new Exception("Inga uppgifter i db.settings");
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
    catch {
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
    Console.WriteLine(    "Hello! \n"
                      +   "Thanks for using this app.\n" +
                          "Please enter your heidi-username and password.");
    Console.WriteLine();
    Console.Write(        "Username: ");

    var inputU = Console.ReadLine();

    Console.Write(          "Password: ");

    var inputP = Console.ReadLine();

    Console.WriteLine();

    ///connectionstringen byggs
    string newConn = "server=localhost;user id=" + inputU + ";password=" + inputP + ";";
    /// den fullständiga med DB till global
    Global.CompleteConnectionString = "server=localhost;user id=" + inputU + ";password=" + inputP + ";database=gnu;";

    return newConn;
}

static void ReadUnOpenEmails(MariaContext _newContext, string ConnectionString)
{
    var me = _newContext.MySettings.First();
    using (var client = new Pop3Client())
    {
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        client.Connect("pop.gmail.com", 995, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        client.Authenticate(me.Email, me.Password);

        for (int i = 0; i < client.Count; i++)
        {
            var message = client.GetMessage(i);
            var subjet = message.Subject;
            var body = message.GetTextBody(MimeKit.Text.TextFormat.Text);
            string[] relativData = body.Split("XYXY/(/(XYXY7");
            string decryptedMess = AesCryption.Decrypt(relativData[0], me.Secret);
            string[] Data = decryptedMess.Split("\"");
            var LocalDate = _newContext.LastUpdates.First();
            string[] Sub = subjet.Split("/()/");
            DateTime IncomeDate = Convert.ToDateTime(Sub[0]);
            if (IncomeDate > LocalDate.TimeSet)
            {
                switch (Sub[1])
                {
                    case "DELETE":
                        DbCommand.CreateCommand(decryptedMess, Global.CompleteConnectionString);
                        break;

                    case "PUT":
                        DbCommand.CreateCommand(decryptedMess, Global.CompleteConnectionString);
                        break;

                    default:
                        DbCommand.CreateCommand(decryptedMess, Global.CompleteConnectionString);
                        break;
                }
                LocalDate.TimeSet = IncomeDate;
                _newContext.LastUpdates.Update(LocalDate);
                _newContext.SaveChanges();
            }

        }
    }
}


