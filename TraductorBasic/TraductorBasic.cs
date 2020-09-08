using System;
using System.Collections.Generic;
using System.IO;

/*
 * Manzanedo Valle, Francisco David
 * Práctica evaluable Temas 8-9
 * Apartado 1 Sí
 * Apartado 2 Sí
 * 
 */
public class TraductorBasic
{
    public static void Main(string[] args)
    {
        string nomFichero;// Nombre del fichero: CodigoBASIC.txt

        nomFichero = PedirCadenaNoVacia("Introduce nombre del fichero: ");
        List<InstruccionBASIC> traductorBasic = CargarInstrucciones(nomFichero);
        //Si se ha conseguido leer el fichero, lo ordenamos, si no, se devuelve la lista vacía, 
        // avisamos y salimos del programa.
        if (traductorBasic.Count > 0) traductorBasic.Sort();
        else
        {
            Console.WriteLine("Se ha producido un error..");
            Environment.Exit(-1);
        }
        Console.Clear();
        //Convertivos el fichero de código BASIC en un fichero de salida en código C#
        TraducirFichero(traductorBasic, nomFichero);
        //Función auxiliar para formatear la salida por consola
        FormatearSalidaConsola(traductorBasic);

        //Terminamos la ejecución del programa
        int posX = 0; int posY = 25;
        Console.SetCursorPosition(posX, posY);
        Console.WriteLine("Fin del programa.\nPulsa una tecla para finalizar...");
        Console.ReadKey();
        Console.Clear();

    }

    //Función para verificar una cadena no vacía
    public static string PedirCadenaNoVacia(string msj)
    {
        string dato;
        do
        {
            Console.Write(msj);
            dato = Console.ReadLine();

        } while (String.IsNullOrEmpty(dato));

        return dato;
    }

    //Función para cargar el contendio del fichero en una Lista
    private static List<InstruccionBASIC> CargarInstrucciones(string nomFichero)
    {
        List<InstruccionBASIC> traductor = new List<InstruccionBASIC>();
        Comando comando;
        bool errorLectura;

        if (!File.Exists(nomFichero)) errorLectura = true;
        else
        {
            errorLectura = false;
            try
            {
                using StreamReader fichero = new StreamReader(nomFichero);
                string linea = fichero.ReadLine();
                while (linea != null)
                {
                    if (linea.Length > 0)//Descartamos los espacios en blanco
                    {
                        int numInstruccion = Convert.ToInt32(linea.Substring(0, linea.IndexOf(" ")));
                        comando = linea.Substring(linea.IndexOf(" ") + 1, 5) == "INPUT" ? Comando.INPUT : Comando.PRINT;
                        string parametro = linea.Substring(8);
                        // Creamos un nuevo Objeto InstruccioBASIC y lo añadimos a la lista
                        traductor.Add(new InstruccionBASIC(numInstruccion, comando, parametro));
                       
                        linea = fichero.ReadLine();

                    }else linea = fichero.ReadLine();

                }fichero.Close();//Aunque utilizando el bloque "using" se cierra de forma automática
            }
            catch (IOException) { errorLectura = true; }

            catch (Exception) { errorLectura = true; }
            //Si hay un error de lectura, limpiamos la Lista
            if (errorLectura) traductor.Clear();
        }
        return traductor;
    }

    //Función para traducir el fichero de código BASIC a código C#
    private static void TraducirFichero(List<InstruccionBASIC> traductorBasic, string nombreFichero)
    {
        const string EXTENSION = ".cs";
        string nuevoNomFichero = nombreFichero + EXTENSION;
        StreamWriter fichero = new StreamWriter(nuevoNomFichero);

        //Cadena donde almacenados la líneas que vamos escribiendo con sus correspondientes espacios
        string linea;

        fichero.WriteLine("using System;");
        fichero.WriteLine();
        fichero.WriteLine("class Programa");
        fichero.WriteLine("{");
        linea = EspaciosEnBlanco(4, "static void Main()");
        fichero.WriteLine(linea);
        linea = EspaciosEnBlanco(4, "{");
        fichero.WriteLine(linea);

        for (int i = 0; i < traductorBasic.Count; i++)
        {
            if (traductorBasic[i].Comando.ToString().Equals("PRINT"))
            {
                linea = EspaciosEnBlanco(8, " Console.WriteLine(" + traductorBasic[i].Parametro + ");");
                fichero.WriteLine(linea);
            }
            else if (traductorBasic[i].Comando.ToString().Equals("INPUT") && (!traductorBasic[i].Parametro.StartsWith("\"")))
            {
                linea = EspaciosEnBlanco(8, " int " + traductorBasic[i].Parametro +" = Convert.ToInt32(Console.ReadLine());");
                fichero.WriteLine(linea);
            }
        }
        linea = EspaciosEnBlanco(4, "}");
        fichero.WriteLine(linea);
        fichero.WriteLine("}");
        fichero.Close();//Cerramos el fichero
    }


    //Función encargada de añadir los espacios en blanco correspondientes a cada línea
    private static string EspaciosEnBlanco(byte numEpacios,string cadena)
    {
        string resultado;

        if (numEpacios == 4)
            resultado = "    " + cadena;//Cuatro espacios
        else resultado = "        " + cadena;//Ocho espacios

        return resultado;
    }
    //Función auxiliar para formatear la salida por consola y posicionar el código correspondiente
    private static void FormatearSalidaConsola(List<InstruccionBASIC> traductorBasic)
    {
        //Posicionar las cabeceras de la consola
        int posX = 20; int posY = 0;
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("CODIGO BASIC");
        posX = 80; posY = 0;
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("CODIGO C#");

        //Dividimos la consola en dos a través de barras en color blanco
        posX = 50;
        for (int i = 0; i < 20; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(posX, i);
            Console.WriteLine("|");
        }
        //Imprimimos la Lista de instrucciones BASIC
        posX = 0; posY = 2;
        for (int j = 0; j < traductorBasic.Count; j++)
        {
            Console.SetCursorPosition(posX, j + posY);
            traductorBasic[j].Imprimir();
        }
        //Imprimimos la lista de instrucciones BASIC traducidas a C#
        posX = 52; posY = 2;
        for (int z = 0; z < traductorBasic.Count; z++)
        {
            Console.SetCursorPosition(posX, z + posY);
            traductorBasic[z].ImprimirAuxiliar();
        }
    }
}
