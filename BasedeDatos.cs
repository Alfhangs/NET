using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Proyecto_Final
{
    public class BasedeDatos
    {
        private string cadenaConexion;
        private MySqlConnection conexion;
        //private MySqlCommand comando;
        private MySqlDataReader lector;

        public BasedeDatos()
        {
            
            // Pasos 1 y 2
            var sb = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                UserID = "root",
                Password = "",
                Port = 3306,
                Database = "JardineriaOnline"
            };

            cadenaConexion = sb.ConnectionString;
            conexion = new MySqlConnection(cadenaConexion);
           // comando =  new MySqlCommand();  
        }

        public BasedeDatos(string connectionString)
        {
            // Pasos 1 y 2 
            cadenaConexion = connectionString;
            conexion = new MySqlConnection(cadenaConexion);
            //comando = new MySqlCommand();
        }

        public void Abrir()
        {
           // Paso 3
            conexion.Open();
        }

        public void Cerrar()
        {
            // Punto 7
            conexion.Close();
            conexion.Dispose();
        }

        public MySqlDataReader EjecutarSelect(MySqlCommand comando)
        {
            // Paso 5
            comando.Connection = conexion;
            comando.CommandTimeout = 60;
            lector = comando.ExecuteReader();
            return lector;
        }

        public int EjecutarIUD(MySqlCommand comando)
        {
            // Paso 5
            comando.Connection = conexion;
            comando.CommandTimeout = 60;
            int filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas;
        }
    }
}
