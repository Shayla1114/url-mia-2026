using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("INFORMACION DE ARCHIVOS Y DIRECTORIOS");
        Console.WriteLine();

        Console.Write("Ingrese una ruta: ");
        string ruta = Console.ReadLine();

        Console.WriteLine();

        if (File.Exists(ruta))
        {
            Console.WriteLine("La ruta corresponde a un archivo.");

            FileInfo archivo = new FileInfo(ruta);

            Console.WriteLine("Nombre: " + archivo.Name);
            Console.WriteLine("Tamaño: " + archivo.Length + " bytes");
            Console.WriteLine("Ultima modificacion: " + archivo.LastWriteTime);
            Console.WriteLine("Atributos: " + archivo.Attributes);
        }
        else if (Directory.Exists(ruta))
        {
            Console.WriteLine("La ruta corresponde a un directorio.");
            Console.WriteLine();

            string[] archivos = Directory.GetFiles(ruta);

            Console.WriteLine("Archivos encontrados:");

            foreach (string archivoRuta in archivos)
            {
                FileInfo archivo = new FileInfo(archivoRuta);

                Console.WriteLine();
                Console.WriteLine("Nombre: " + archivo.Name);
                Console.WriteLine("Tamaño: " + archivo.Length + " bytes");
                Console.WriteLine("Ultima modificacion: " + archivo.LastWriteTime);
                Console.WriteLine("Atributos: " + archivo.Attributes);
            }
        }
        else
        {
            Console.WriteLine("La ruta ingresada no existe.");
        }
    }
}