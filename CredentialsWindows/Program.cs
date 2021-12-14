using Library.HelpClasses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Welcome_Settings;


string myConn = EnterCredentials();
WriteToJson("ConnectionStrings:Defaultconnection", myConn);

Environment.ExitCode = 0;

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
    string newConn1 = "server=localhost;user id=" + inputU + ";password=" + inputP + ";database=gnu;";
    return newConn1;
}

static void WriteToJson(string sectionPathKey, string value)
{
    string path = @"..\GnuOne\appsettings.json";
    string fullpath = Path.GetFullPath(path);
    //C:\Users\bergl\source\repos\GnuOne\GnuOne\appsettings.json

    Console.WriteLine(Path.GetFullPath(path));
    try
    {
        var filePath = fullpath;
        string json = File.ReadAllText(filePath);
        dynamic? jsonObj = JsonConvert.DeserializeObject(json);

        SetValueRecursively(sectionPathKey, jsonObj, value);

        string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
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