using GnuOne.Data;

MariaContext context = new MariaContext("server=localhost;user id=root;password=Hejsan123!;database=gnu;");


var Disc = context.Discussion.Where(x => x.user == "Chris").ToList();



foreach (var item in Disc)
{
    Console.WriteLine(item.ToString());
    Console.WriteLine("-------");
    Console.WriteLine(item.user + ";;");
    Console.WriteLine(item.headline + ";;");
    Console.WriteLine(item.discussiontext + ";;");
    Console.WriteLine(item.createddate + ";;");
    Console.WriteLine("--------");
}



