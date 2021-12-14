


using Library.HelpClasses;
using MailKit.Net.Pop3;
using Welcome_Settings;




using (MariaContext context = new MariaContext(Global.CompleteConnectionString))
{
    int i = 0;
    while (true)
    {
        ReadUnOpenEmails(context, Global.CompleteConnectionString);
        i++;
        Console.WriteLine($"Read email {i} times");
        Thread.Sleep(5000);
    }
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