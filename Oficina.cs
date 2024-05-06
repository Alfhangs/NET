using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Proyecto_Final
{

    /*
	CREATE TABLE `oficina` (
	`codigo_oficina` VARCHAR(10) NOT NULL COLLATE 'utf8mb4_general_ci',
	`ciudad` VARCHAR(30) NOT NULL COLLATE 'utf8mb4_general_ci',
	`pais` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
	`region` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`codigo_postal` VARCHAR(10) NOT NULL COLLATE 'utf8mb4_general_ci',
	`telefono` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_general_ci',
	`linea_direccion1` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
	`linea_direccion2` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci'
	*/



    public class Oficina
    {
        private string codigo_oficina;
        private string ciudad;
        private string pais;
        private string region;
        private string codigo_postal;
        private string telefono;
        private string linea_direccion1;
        private string linea_direccion2;
        public string Codigo_oficina { get => codigo_oficina; set => codigo_oficina = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Pais { get => pais; set => pais = value; }
        public string Region { get => region; set => region = value; }
        public string Codigo_postal { get => codigo_postal; set => codigo_postal = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Linea_direccion1 { get => linea_direccion1; set => linea_direccion1 = value; }
        public string Linea_direccion2 { get => linea_direccion2; set => linea_direccion2 = value; }
        public Oficina()
        {
        }

        public Oficina(string codigo_oficina, string ciudad, string pais, string region, string codigo_postal, string telefono, string linea_direccion1, string linea_direccion2)
        {
            Codigo_oficina = codigo_oficina;
            Ciudad = ciudad;
            Pais = pais;
            Region = region;
            Codigo_postal = codigo_postal;
            Telefono = telefono;
            Linea_direccion1 = linea_direccion1;
            Linea_direccion2 = linea_direccion2;
        }

        public override bool Equals(object obj)
        {
            return obj is Oficina oficina &&
                   Codigo_oficina == oficina.Codigo_oficina &&
                   Ciudad == oficina.Ciudad &&
                   Pais == oficina.Pais &&
                   Region == oficina.Region &&
                   Codigo_postal == oficina.Codigo_postal &&
                   Telefono == oficina.Telefono &&
                   Linea_direccion1 == oficina.Linea_direccion1 &&
                   Linea_direccion2 == oficina.Linea_direccion2;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Codigo_oficina, Ciudad, Pais, Region, Codigo_postal, Telefono, Linea_direccion1, Linea_direccion2);
        }

        public override string ToString()
        {
            return $"{Codigo_oficina}:{Ciudad}-{Pais}";
        }

        public static List<Oficina> ListarOficinas()
        {
            List<Oficina> lista = new List<Oficina>(); // lo que devolverá
            // Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            bd.Abrir();

            //Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM oficina";
            // Paso 5
            lector = bd.EjecutarSelect(cmd);

            // Paso 6 
            while (lector.Read())
            {

                //Console.WriteLine($"{lector.GetString("codigo_oficina")}{lector.GetString("ciudad")}");
                lista.Add(new Oficina(lector.GetString("codigo_oficina"), lector.GetString("ciudad"), lector.GetString("pais"),
                                      lector.IsDBNull(3) ? "" : lector.GetString("region"), lector.GetString("codigo_postal"), lector.GetString("telefono"),
                                      lector.GetString("linea_direccion1"), lector.GetString("linea_direccion2")));
            }
            lector.Close();
            //Paso 7
            bd.Cerrar();

            return lista;

        }
        public static List<Oficina> ListaOficina(string code)
        {
            List<Oficina> lista = new List<Oficina>();
            //Pasos 1 y 2
            BasedeDatos database = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            database.Abrir();

            // Paso 4
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "SELECT * FROM oficina WHERE codigo_oficina = ?codigo_oficina";
            comando.Parameters.Add("?codigo_oficina", MySqlDbType.VarChar).Value = code;


            // Paso 5
            lector = database.EjecutarSelect(comando);

            // Paso 6 
            while (lector.Read())
            {
                //Console.WriteLine($"{lector.GetString("codigo_oficina")}{lector.GetString("ciudad")}");
                lista.Add(new Oficina(lector.GetString("codigo_oficina"), lector.GetString("ciudad"), lector.GetString("pais"),
                                      lector.IsDBNull(3) ? "" : lector.GetString("region"), lector.GetString("codigo_postal"), lector.GetString("telefono"),
                                      lector.GetString("linea_direccion1"), lector.GetString("linea_direccion2")));
            }

            lector.Close();
            //Paso 7
            database.Cerrar();

            return lista;
        }
        public int Insertar()
        {
            // Hace la insert en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos database = new BasedeDatos();
            // Paso 3
            database.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO oficina (codigo_oficina,ciudad,pais,region,codigo_postal,telefono,linea_direccion1,linea_direccion2) " +
                              "VALUES(?codigo_oficina,?ciudad,?pais,?region,?codigo_postal,?telefono,?linea_direccion1,?linea_direccion2) ";


            cmd.Parameters.Add("?codigo_oficina", MySqlDbType.VarChar).Value = this.Codigo_oficina;
            cmd.Parameters.Add("?ciudad", MySqlDbType.VarChar).Value = this.Ciudad;
            cmd.Parameters.Add("?pais", MySqlDbType.VarChar).Value = this.Pais;
            cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = this.Region;
            cmd.Parameters.Add("?codigo_postal", MySqlDbType.VarChar).Value = this.Codigo_postal;
            cmd.Parameters.Add("?telefono", MySqlDbType.VarChar).Value = this.Telefono;
            cmd.Parameters.Add("?linea_direccion1", MySqlDbType.VarChar).Value = this.Linea_direccion1;
            cmd.Parameters.Add("?linea_direccion2", MySqlDbType.VarChar).Value = this.Linea_direccion2;

            filasAfectadas = database.EjecutarIUD(cmd);

            // Paso 7
            database.Cerrar();

            return filasAfectadas;
        }

        public int Actualizar()
        {
            // Hace la actualización en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE oficina SET ciudad = ?ciudad, " +
                                                "pais = ?pais, " +
                                                "region = ?region, " +
                                                "codigo_postal = ?codigo_postal, " +
                                                "telefono = ?telefono, " +
                                                "linea_direccion1 = ?linea_direccion1, " +
                                                "linea_direccion2 = ?linea_direccion2 " +
                              "WHERE codigo_oficina = ?codigo_oficina";

            cmd.Parameters.Add("?codigo_oficina", MySqlDbType.VarChar).Value = this.Codigo_oficina;
            cmd.Parameters.Add("?ciudad", MySqlDbType.VarChar).Value = this.Ciudad;
            cmd.Parameters.Add("?pais", MySqlDbType.VarChar).Value = this.Pais;
            cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = this.Region;
            cmd.Parameters.Add("?codigo_postal", MySqlDbType.VarChar).Value = this.Codigo_postal;
            cmd.Parameters.Add("?telefono", MySqlDbType.VarChar).Value = this.Telefono;
            cmd.Parameters.Add("?linea_direccion1", MySqlDbType.VarChar).Value = this.Linea_direccion1;
            cmd.Parameters.Add("?linea_direccion2", MySqlDbType.VarChar).Value = this.Linea_direccion2;

            filasAfectadas = bd.EjecutarIUD(cmd);

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;
        }
        public int Borrar()
        {
            // Hace la actualización en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "DELETE FROM oficina "+
                              "WHERE codigo_oficina = ?codigo_oficina";

            cmd.Parameters.Add("?codigo_oficina", MySqlDbType.VarChar).Value = this.Codigo_oficina;

            filasAfectadas = bd.EjecutarIUD(cmd);

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;
        }
    }
}
