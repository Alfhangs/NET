using System;
using System.Collections.Generic;
using System.IO;

namespace Proyecto_Final
{
    public class ListaIncidencias
    {
        private static List<Incidencia> incidencias;

        public ListaIncidencias()
        {
            incidencias = new List<Incidencia>();
        }

        public static void AddIncidencia(DateTime d,string informacion)
        {
            incidencias.Add(new Incidencia(d, informacion));
        }

        public static void ListarIncidencias()
        {
            foreach (Incidencia i in incidencias)
            {
                Console.WriteLine(i);
            }
        }
        public static void GuardaIncidencias()
        {
            string nombre;

            nombre = Convert.ToString(DateTime.Now.Year) + 
                    Convert.ToString(DateTime.Now.Month)
                    + Convert.ToString(DateTime.Now.Day);
            try
            {
                StreamWriter writer = new StreamWriter(nombre, true);
                foreach(Incidencia i in incidencias)
                {
                    writer.WriteLine(i);
                }
                writer.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("Error con el fichero " + nombre);
            }

            
        }

    }
}
