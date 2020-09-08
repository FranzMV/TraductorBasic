using System;

class Programa
{
    static void Main()
    {
         Console.WriteLine( "Escribe un numero");
         int  n1 = Convert.ToInt32(Console.ReadLine());
         Console.WriteLine( "Escribe otro numero");
         int  n2 = Convert.ToInt32(Console.ReadLine());
         Console.WriteLine( "Su suma es");
         Console.WriteLine( n1+n2);
    }
}
