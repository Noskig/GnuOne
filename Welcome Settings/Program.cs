// See https://aka.ms/new-console-template for more information
using Library;
using Library.HelpClasses;
using MailKit.Net.Pop3;
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
///
/// 
/// 
/// ---------------------------------------------------------------
/// GJORT:
/// Ändra klass namn SqlScript till Global (klassen innehåller nu sqlscript + sätter connectionstringen)
/// Implementerat evighetsLoop med mailreader vart 5 sekund.
/// Implementerat AesCryption i HelpClass.
///------------------------------------------------
/// 







Global.ConnectionString = EnterCredentials(); 
MariaContext context = new MariaContext(Global.ConnectionString);
CreateDatabase(/*CheckIfDatabaseExist(context),*/ Global.ConnectionString);
MariaContext _newContext = new MariaContext(Global.CompleteConnectionString);
CheckMailCredentials(_newContext);

/// check if mailadress exist in db
/// if yes - start reading mail
/// if no - Send to frontend to enter and then save mailadress



static void CheckMailCredentials(MariaContext _newContext)
{
    //finns det nått finns det allt - Frontend validering
    if (_newContext.MySettings.Any())
    {
        int i = 0;
        Console.WriteLine("Endless Mail-read LOOOP begins now.");
        while (true)
        {
            ReadUnOpenEmails(_newContext, Global.CompleteConnectionString);
            i++;
            Console.WriteLine($"Read email {i} times");
            Thread.Sleep(5000);
        }
    }
    else
    {
        Console.WriteLine("Här måste du fylla i dina uppgifter på FRONTEND");
        ///skicka till frontend create mail - som pingar api?
    }

}
static void CreateDatabase(/*bool created,*/ string connection)
{
    DbCommand.CreateCommand(Global.sql, connection);
}
//static bool CheckIfDatabaseExist(MariaContext context)
//{
//    if (context.Database.CanConnect()) //FEEEEEEL (Stoppar, hittar inga databas)
//    {
//        return true;
//    }
//    else return false;

//}
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

        string newConn = "server=localhost;user id=" + inputU + ";password=" + inputP + ";";
        Global.CompleteConnectionString = "server=localhost;user id=" + inputU + ";password=" + inputP + ";database=gnu;"; 

    return newConn;
        Console.WriteLine(newConn);
        Console.WriteLine("thanks");
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


