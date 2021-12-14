using GnuOne.Data;
using Library.HelpClasses;
using MailKit.Net.Pop3;
using Microsoft.EntityFrameworkCore;
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
StartCredential();

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

app.MapFallbackToFile("index.html"); 

int a = 0; //Visualiserar att mailfunktionen rullar.
var loop1Task = Task.Run(async () => {
    while (true)
    {
        using (MariaContext context = new MariaContext(_connectionstring))
        {
            ReadUnOpenEmails(context, _connectionstring);
            await Task.Delay(5000);
            a++;
            Console.WriteLine(a);
        }
    }
});

app.Run();






static void StartCredential()
{
    string path = @"..\Welcome Settings\bin\debug\net6.0\Welcome Settings.exe";
    string fullpath = Path.GetFullPath(path);
    Console.WriteLine(Path.GetFullPath(path));

    Process process = new Process();
    // Configure the process using the StartInfo properties.
    process.StartInfo.FileName = fullpath;
    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
    process.StartInfo.UseShellExecute = true;
    process.StartInfo.CreateNoWindow = false;
    process.Start();
    process.WaitForExit();
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
                        DbCommand.CreateCommand(decryptedMess, ConnectionString);
                        break;

                    case "PUT":
                        DbCommand.CreateCommand(decryptedMess, ConnectionString);
                        break;

                    default:
                        DbCommand.CreateCommand(decryptedMess, ConnectionString);
                        break;
                }
                LocalDate.TimeSet = IncomeDate;
                _newContext.LastUpdates.Update(LocalDate);
                _newContext.SaveChanges();
            }

        }
    }
}

