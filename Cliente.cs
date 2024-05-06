using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Proyecto_Final
{
    public class Cliente 
    {
        private int codigo_cliente;
        private string nombre_cliente;
        private string nombre_contacto;
        private string apellido_contacto;
        private string telefono;
        private string fax;
        private string linea_direccion1;
        private string linea_direccion2;
        private string ciudad;
        private string region;
        private string pais;
        private string codigo_postal;
        private int codigo_empleado_rep_ventas;
        private decimal limite_credito;
        private string pass;

        public int Codigo_cliente { get => codigo_cliente; set => codigo_cliente = value; }
        public string Nombre_cliente { get => nombre_cliente; set => nombre_cliente = value; }
        public string Nombre_contacto { get => nombre_contacto; set => nombre_contacto = value; }
        public string Apellido_contacto { get => apellido_contacto; set => apellido_contacto = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Fax { get => fax; set => fax = value; }
        public string Linea_direccion1 { get => linea_direccion1; set => linea_direccion1 = value; }
        public string Linea_direccion2 { get => linea_direccion2; set => linea_direccion2 = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Region { get => region; set => region = value; }
        public string Pais { get => pais; set => pais = value; }
        public string Codigo_postal { get => codigo_postal; set => codigo_postal = value; }
        public int Codigo_empleado_rep_ventas { get => codigo_empleado_rep_ventas; set => codigo_empleado_rep_ventas = value; }
        public decimal Limite_credito { get => limite_credito; set => limite_credito = value; }
        public string Pass { get => pass; set => pass = value; }

        public Cliente()
        {

        }

        public Cliente(int codigo_cliente, string nombre_cliente, string nombre_contacto, string apellido_contacto, string telefono, string fax, string linea_direccion1, string linea_direccion2, string ciudad, string region, string pais, string codigo_postal, int codigo_empleado_rep_ventas, decimal limite_credito, string pass)
        {
            this.codigo_cliente = codigo_cliente;
            this.nombre_cliente = nombre_cliente;
            this.nombre_contacto = nombre_contacto;
            this.apellido_contacto = apellido_contacto;
            this.telefono = telefono;
            this.fax = fax;
            this.linea_direccion1 = linea_direccion1;
            this.linea_direccion2 = linea_direccion2;
            this.ciudad = ciudad;
            this.region = region;
            this.pais = pais;
            this.codigo_postal = codigo_postal;
            this.codigo_empleado_rep_ventas = codigo_empleado_rep_ventas;
            this.limite_credito = limite_credito;
            this.pass = pass;
        }

        

        public override string ToString()
        {
            return $"{Codigo_cliente,-4} {Nombre_cliente,-30} {Apellido_contacto,-15} {Ciudad,-20} ";
        }

        public override bool Equals(object obj)
        {
            return obj is Cliente cliente &&
                   Codigo_cliente == cliente.Codigo_cliente &&
                   Nombre_cliente == cliente.Nombre_cliente &&
                   Nombre_contacto == cliente.Nombre_contacto &&
                   Apellido_contacto == cliente.Apellido_contacto &&
                   Telefono == cliente.Telefono &&
                   Fax == cliente.Fax &&
                   Linea_direccion1 == cliente.Linea_direccion1 &&
                   Linea_direccion2 == cliente.Linea_direccion2 &&
                   Ciudad == cliente.Ciudad &&
                   Region == cliente.Region &&
                   Pais == cliente.Pais &&
                   Codigo_postal == cliente.Codigo_postal &&
                   Codigo_empleado_rep_ventas == cliente.Codigo_empleado_rep_ventas &&
                   Limite_credito == cliente.Limite_credito &&
                   Pass == cliente.Pass;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Codigo_cliente);
            hash.Add(Nombre_cliente);
            hash.Add(Nombre_contacto);
            hash.Add(Apellido_contacto);
            hash.Add(Telefono);
            hash.Add(Fax);
            hash.Add(Linea_direccion1);
            hash.Add(Linea_direccion2);
            hash.Add(Ciudad);
            hash.Add(Region);
            hash.Add(Pais);
            hash.Add(Codigo_postal);
            hash.Add(Codigo_empleado_rep_ventas);
            hash.Add(Limite_credito);
            hash.Add(Pass);
            return hash.ToHashCode();
        }

        public static List<Cliente> ListaCliente()
        {
            List<Cliente> lista = new List<Cliente>(); // lo que devolverá
            // Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            bd.Abrir();

            //Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM cliente";
            // Paso 5
            lector = bd.EjecutarSelect(cmd);

            // Paso 6 
            while (lector.Read())
            {

                //Console.WriteLine($"{lector.GetString("codigo_oficina")}{lector.GetString("ciudad")}");
                lista.Add(new Cliente(lector.GetInt32("codigo_cliente"), lector.GetString("nombre_cliente"), lector.IsDBNull(2) ? "" : lector.GetString("nombre_contacto"),
                                      lector.IsDBNull(3) ? "" : lector.GetString("apellido_contacto"), lector.GetString("telefono"), lector.GetString("fax"),
                                      lector.GetString("linea_direccion1"), lector.IsDBNull(7) ? "" : lector.GetString("linea_direccion2"),
                                      lector.GetString("ciudad"), lector.IsDBNull(9) ? "" : lector.GetString("region"),
                                      lector.IsDBNull(10) ? "" : lector.GetString("pais"),
                                      lector.IsDBNull(11) ? "" : lector.GetString("codigo_postal"),
                                      lector.IsDBNull(12) ? 0 : lector.GetInt32("codigo_empleado_rep_ventas"),
                                      lector.IsDBNull(13) ? 0 : lector.GetDecimal("limite_credito"),
                                      lector.GetString("pass")));
            }

            //Paso 7
            lector.Close();
            bd.Cerrar();

            return lista;

        }
        public static List<Cliente> ListaCliente(int codigo)
        {
            List<Cliente> lista = new List<Cliente>(); // lo que devolverá
            // Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            bd.Abrir();

            //Paso 4
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "SELECT * FROM cliente WHERE codigo_cliente = ?codigo_cliente";
            comando.Parameters.Add("?codigo_cliente", MySqlDbType.Int32).Value = codigo;
            // Paso 5
            lector = bd.EjecutarSelect(comando);

            // Paso 6 
            while (lector.Read())
            {

                //Console.WriteLine($"{lector.GetString("codigo_oficina")}{lector.GetString("ciudad")}");
                lista.Add(new Cliente(lector.GetInt32("codigo_cliente"), lector.GetString("nombre_cliente"), lector.IsDBNull(2) ? "" : lector.GetString("nombre_contacto"),
                                      lector.IsDBNull(3) ? "" : lector.GetString("apellido_contacto"), lector.GetString("telefono"), lector.GetString("fax"),
                                      lector.GetString("linea_direccion1"), lector.IsDBNull(7) ? "" : lector.GetString("linea_direccion2"),
                                      lector.GetString("ciudad"), lector.IsDBNull(9) ? "" : lector.GetString("region"),
                                      lector.IsDBNull(10) ? "" : lector.GetString("pais"),
                                      lector.IsDBNull(11) ? "" : lector.GetString("codigo_postal"),
                                      lector.IsDBNull(12) ? 0 : lector.GetInt32("codigo_empleado_rep_ventas"),
                                      lector.IsDBNull(13) ? 0 : lector.GetDecimal("limite_credito"),
                                      lector.GetString("pass")));
            }

            //Paso 7
            lector.Close();
            bd.Cerrar();

            return lista;

        }

        public  int Insertar()
        {
            // Hace la insert en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();
            if(this.Codigo_empleado_rep_ventas!=0)
              cmd.CommandText = "INSERT INTO cliente (nombre_cliente,nombre_contacto,apellido_contacto,telefono,fax,linea_direccion1," +
                                                    "linea_direccion2,ciudad,region,pais,codigo_postal,codigo_empleado_rep_ventas,limite_credito,pass) " +
                              "VALUES(?nombre_cliente,?nombre_contacto,?apellido_contacto,?telefono,?fax,?linea_direccion1," +
                                                    "?linea_direccion2,?ciudad,?region,?pais,?codigo_postal,?codigo_empleado_rep_ventas,?limite_credito,?pass) ";
            else
                cmd.CommandText = "INSERT INTO cliente (nombre_cliente,nombre_contacto,apellido_contacto,telefono,fax,linea_direccion1," +
                                                   "linea_direccion2,ciudad,region,pais,codigo_postal,limite_credito,pass) " +
                             "VALUES(?nombre_cliente,?nombre_contacto,?apellido_contacto,?telefono,?fax,?linea_direccion1," +
                                                   "?linea_direccion2,?ciudad,?region,?pais,?codigo_postal,?limite_credito,?pass) ";



            cmd.Parameters.Add("?nombre_cliente", MySqlDbType.VarChar).Value = this.Nombre_cliente;
            cmd.Parameters.Add("?nombre_contacto", MySqlDbType.VarChar).Value = this.Nombre_contacto;
            cmd.Parameters.Add("?apellido_contacto", MySqlDbType.VarChar).Value = this.Apellido_contacto;
            cmd.Parameters.Add("?telefono", MySqlDbType.VarChar).Value = this.Telefono;
            cmd.Parameters.Add("?fax", MySqlDbType.VarChar).Value = this.Fax;
            cmd.Parameters.Add("?linea_direccion1", MySqlDbType.VarChar).Value = this.Linea_direccion1;
            cmd.Parameters.Add("?linea_direccion2", MySqlDbType.VarChar).Value = this.Linea_direccion2;
            cmd.Parameters.Add("?ciudad", MySqlDbType.VarChar).Value = this.Ciudad;
            cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = this.Region;
            cmd.Parameters.Add("?pais", MySqlDbType.VarChar).Value = this.Pais;
            cmd.Parameters.Add("?codigo_postal", MySqlDbType.VarChar).Value = this.Codigo_postal;
            if(this.Codigo_empleado_rep_ventas!=0)
                cmd.Parameters.Add("?codigo_empleado_rep_ventas", MySqlDbType.Int32).Value = this.Codigo_empleado_rep_ventas;
            cmd.Parameters.Add("?limite_credito", MySqlDbType.Decimal).Value = this.Limite_credito;
            cmd.Parameters.Add("?pass", MySqlDbType.VarChar).Value = this.pass;

            filasAfectadas = bd.EjecutarIUD(cmd);

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;
        }
        
        public   int Actualizar()
        {
            // Hace la actualización en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = "UPDATE cliente SET nombre_cliente=?nombre_cliente,"+
                                                 "nombre_contacto=?nombre_contacto,"+
                                                 "apellido_contacto=?apellido_contacto,"+
                                                 "telefono=?telefono,"+
                                                 "fax=?fax,"+
                                                 "linea_direccion1=?linea_direccion1,"+
                                                 "linea_direccion2=?linea_direccion2,"+
                                                 "ciudad=?ciudad,"+
                                                 "region=?region,"+
                                                 "pais=?pais,"+
                                                 "codigo_postal=?codigo_postal,"+
                                                 "codigo_empleado_rep_ventas=?codigo_empleado_rep_ventas,"+
                                                 "limite_credito=?limite_credito,"+
                                                 "pass=?pass "+
                              "WHERE codigo_cliente = ?codigo_cliente";

            cmd.Parameters.Add("?codigo_cliente", MySqlDbType.VarChar).Value = this.Codigo_cliente;
            cmd.Parameters.Add("?nombre_cliente", MySqlDbType.VarChar).Value = this.Nombre_cliente;
            cmd.Parameters.Add("?nombre_contacto", MySqlDbType.VarChar).Value = this.Nombre_contacto;
            cmd.Parameters.Add("?apellido_contacto", MySqlDbType.VarChar).Value = this.Apellido_contacto;
            cmd.Parameters.Add("?telefono", MySqlDbType.VarChar).Value = this.Telefono;
            cmd.Parameters.Add("?fax", MySqlDbType.VarChar).Value = this.Fax;
            cmd.Parameters.Add("?linea_direccion1", MySqlDbType.VarChar).Value = this.Linea_direccion1;
            cmd.Parameters.Add("?linea_direccion2", MySqlDbType.VarChar).Value = this.Linea_direccion2;
            cmd.Parameters.Add("?ciudad", MySqlDbType.VarChar).Value = this.Ciudad;
            cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = this.Region;
            cmd.Parameters.Add("?pais", MySqlDbType.VarChar).Value = this.Pais;
            cmd.Parameters.Add("?codigo_postal", MySqlDbType.VarChar).Value = this.Codigo_postal;
            cmd.Parameters.Add("?codigo_empleado_rep_ventas", MySqlDbType.Int32).Value = this.Codigo_empleado_rep_ventas;
            cmd.Parameters.Add("?limite_credito", MySqlDbType.Decimal).Value = this.Limite_credito;
            cmd.Parameters.Add("?pass", MySqlDbType.VarChar).Value = this.pass;

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
            cmd.CommandText = "DELETE FROM cliente " +
                              "WHERE codigo_cliente = ?codigo_cliente";

            cmd.Parameters.Add("?codigo_cliente", MySqlDbType.VarChar).Value = this.Codigo_cliente;

            filasAfectadas = bd.EjecutarIUD(cmd);

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;
        }

    }
}
