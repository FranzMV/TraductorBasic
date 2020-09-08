using System;

public enum Comando { PRINT, INPUT };

public class InstruccionBASIC : IComparable<InstruccionBASIC>
{
    //Atributos de clase
    private int numInstruccion;
    private string parametros;
    Comando comando;

    //Constructor con parámetros
    public InstruccionBASIC(int numInstruccion, Comando comando, string parametros)
    {
        this.numInstruccion = numInstruccion;
        this.parametros = parametros;
        this.comando = comando;
    }

    //Métodos de acceso Getters y Setters
    public string Parametro { get => parametros; set => parametros = value; }
    public Comando Comando { get => comando; set => comando = value; }
    public int NumInstruccion { get => numInstruccion; set => numInstruccion = value; }

    //Método de la Interfaz Icomparable para comparar por número de instrucción
    public int CompareTo(InstruccionBASIC other) => NumInstruccion.CompareTo(other.NumInstruccion);

    //Sobrecarga del método toString para obtener la información del objeto
    public override string ToString() => NumInstruccion + " " + Comando + " " + Parametro;
    
    //Función para imprimir el código BASIC almacenado en la lista de instrucciones
    public void Imprimir()
    {

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(NumInstruccion);
        if (Comando.ToString().Equals("PRINT") && Parametro.Contains("\""))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ");
            Console.Write(Comando);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.Write(Parametro);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ");
            Console.Write(Comando);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Parametro);
        }
    }

    //Función auxiliar que permite imprimir el codigo traducido de BASIC a C# en pantalla
    public  void ImprimirAuxiliar()
    {
        if (Comando.ToString().Equals("PRINT") && Parametro.Contains("\""))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Console.WriteLine");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Parametro);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(");");
        }
        else if (Comando.ToString().Equals("INPUT") && !Parametro.Contains("\""))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("int ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Parametro+" = ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Convert.ToInt32");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Console.ReadLine");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("());");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Console.WriteLine");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("("+Parametro+");");
        }
    }
}
  


