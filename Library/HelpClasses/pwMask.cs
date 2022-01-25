
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpClasses
{
    //Disguise password in Console.
    public class pwMask
    {
        public static string pwMasker()
        {
            var inputP = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && inputP.Length > 0)
                {
                    Console.Write("\b \b");
                    inputP = inputP[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    inputP += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return inputP;
        }
    }
}
