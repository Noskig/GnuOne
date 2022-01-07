DateTime foo = DateTime.Now;
long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
int ID = Convert.ToInt32(unixTime);


Console.WriteLine(ID);