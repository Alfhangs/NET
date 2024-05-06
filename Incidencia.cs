using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Final
{
    public class Incidencia
    {
        DateTime hora;
        string operacion;

        public DateTime Hora { get => hora; set => hora = value; }
        public string Operacion { get => operacion; set => operacion = value; }
        public Incidencia(DateTime hora, string operacion)
        {
            this.hora = hora;
            this.operacion = operacion;
        }

        public override string ToString()
        {
            return $"{hora}: {operacion}";
        }

       

    }
}
