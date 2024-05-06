using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Proyecto_Final
{
    public class Pedido
    {
        private int codigo_pedido;
        private DateTime fecha_pedido;
        private DateTime fecha_esperada;
        private DateTime fecha_entrega;
        private string estado;
        private string comentarios;
        private int codigo_cliente;

        private Cliente cliente;
        private List<DetallePedido> detalles;

        public int Codigo_pedido { get => codigo_pedido; set => codigo_pedido = value; }
        public DateTime Fecha_pedido { get => fecha_pedido; set => fecha_pedido = value; }
        public DateTime Fecha_esperada { get => fecha_esperada; set => fecha_esperada = value; }
        public DateTime Fecha_entrega { get => fecha_entrega; set => fecha_entrega = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public int Codigo_cliente { get => codigo_cliente; set => codigo_cliente = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public List<DetallePedido> Detalles { get => detalles; set => detalles = value; }

        public Pedido()
        {

        }

        public Pedido(int codigo_pedido, DateTime fecha_pedido, DateTime fecha_esperada, DateTime fecha_entrega, string estado, string comentarios, int codigo_cliente)
        {
            this.codigo_pedido = codigo_pedido;
            this.fecha_pedido = fecha_pedido;
            this.fecha_esperada = fecha_esperada;
            this.fecha_entrega = fecha_entrega;
            this.estado = estado;
            this.comentarios = comentarios;
            this.codigo_cliente = codigo_cliente;
            //Inicializar objetos Cliente y Lista de DetallePedido

            cliente = Cliente.ListaCliente(codigo_cliente)[0];
            detalles = DetallePedido.ListaDetalle(codigo_pedido);

        }

        public void Mostrar()
        {
            decimal total = 0;
            int IVA = 21;
            string espacios = new String(' ', 10);

            Console.WriteLine($"{espacios}{"PEDIDO CÓDIGO: "}{Codigo_pedido}");

            Console.WriteLine($"{espacios}----------------------------------------------------------------------------");
            // Mostramos los datos generales del pedido

            Console.WriteLine($"{espacios}{"NOMBRE: ",-7}{Cliente.Nombre_cliente,-40}"+
                $"{"APELLIDOS: ",-10}{Cliente.Apellido_contacto,-20}");

            Console.WriteLine($"{espacios}{"FECHA PEDIDO:",-15}{Fecha_pedido.ToShortDateString(),-32}"+
                $"{"FECHA ENTREGA:",-15}{Fecha_entrega.ToShortDateString(),-15}");

            Console.WriteLine($"{espacios}-----------------------------------------------------------------------------");

            Console.WriteLine($"{espacios}{"LÍNEA",-7}{"PRODUCTO",-40}{"CANTIDAD",-10}{"PRECIO",-10}{"TOTAL",-10}");

            Console.WriteLine($"{espacios}-----------------------------------------------------------------------------");
            // Mostramos cada uno de los productos que hemos comprado

            foreach (DetallePedido linea in detalles)
            {
                total += linea.Cantidad * linea.Precio_unidad;
                Console.Write(espacios);
                Console.WriteLine(linea);
            }
            Console.WriteLine($"{espacios}-----------------------------------------------------------------------------");
            Console.WriteLine($"{espacios}Total sin IVA: {total} IVA(21%):{(total * IVA)/100} Total: {total + (total * IVA)}");


        }

        public override bool Equals(object obj)
        {
            return obj is Pedido pedido &&
                   codigo_pedido == pedido.codigo_pedido &&
                   fecha_pedido == pedido.fecha_pedido &&
                   fecha_esperada == pedido.fecha_esperada &&
                   fecha_entrega == pedido.fecha_entrega &&
                   estado == pedido.estado &&
                   comentarios == pedido.comentarios &&
                   codigo_cliente == pedido.codigo_cliente;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(codigo_pedido, fecha_pedido, fecha_esperada, fecha_entrega, estado, comentarios, codigo_cliente);
        }

        public static List<Pedido> ListaPedido(int codigo)
        {
            List<Pedido> lista = new List<Pedido>(); // lo que devolverá
            // Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            bd.Abrir();

            //Paso 4
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "SELECT * FROM pedido WHERE codigo_pedido = ?codigo_pedido";
            comando.Parameters.Add("?codigo_pedido", MySqlDbType.Int32).Value = codigo;
            // Paso 5
            lector = bd.EjecutarSelect(comando);

            // Paso 6 
            while (lector.Read())
            {
                lista.Add(new Pedido(lector.GetInt32("codigo_pedido"), lector.GetDateTime("fecha_pedido"),
                                     lector.GetDateTime("fecha_esperada"), lector.GetDateTime("fecha_entrega"),
                                     lector.GetString("estado"), lector.GetString("comentarios"),
                                     lector.GetInt32("codigo_cliente")));
            }

            //Paso 7
            lector.Close();
            bd.Cerrar();

            return lista;
        }

        public int Insertar()
        {
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            // No insertamos código de pedido, ya que es auto incremental.
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO pedido (fecha_pedido,fecha_esperada,fecha_entrega,estado,comentarios,codigo_cliente) "+
                              "VALUES(?fecha_pedido,?fecha_esperada,?fecha_entrega,?estado,?comentarios,?codigo_cliente)";
            cmd.CommandType = System.Data.CommandType.Text;
            
            cmd.Parameters.Add("?fecha_pedido", MySqlDbType.DateTime).Value = this.Fecha_pedido;
            cmd.Parameters.Add("?fecha_esperada", MySqlDbType.DateTime).Value = this.Fecha_esperada;
            cmd.Parameters.Add("?fecha_entrega", MySqlDbType.DateTime).Value = this.Fecha_entrega;
            cmd.Parameters.Add("?estado", MySqlDbType.VarChar).Value = this.Estado;
            cmd.Parameters.Add("?comentarios", MySqlDbType.VarChar).Value = this.Comentarios;
            cmd.Parameters.Add("?codigo_cliente", MySqlDbType.Int32).Value = this.Codigo_cliente;


            filasAfectadas =  bd.EjecutarIUD(cmd);
            long codigo_Pedido = cmd.LastInsertedId;
            // RECORRE LISTA DETALLE PEDIDO INSERTANDO.
            // GESTIONAR EL NÚMERO DE lÍNEA y el Código de pedido.
            int filasDetalle = 0;
            foreach (DetallePedido d in detalles)
            {
                
                d.Insertar((int)codigo_Pedido);
                if (filasDetalle> 0)
                { 
                    // CORRECTO
                }
                else { 
                        // ERROR   
                    }
                filasDetalle = 0;
            }

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;
        }
    }
}
