using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class ConsoleHelper
    {

        public static void WriteWithColor( string text,ConsoleColor color = ConsoleColor.White)
        {
         Console.ForegroundColor = color;
            Console.WriteLine(text);    
            Console.ResetColor();
        }
    }
}
