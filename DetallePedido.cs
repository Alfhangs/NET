using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Proyecto_Final
{
    public class DetallePedido
    {
        /*
		CREATE TABLE `detalle_pedido` (
	`codigo_pedido` INT(11) NOT NULL,
	`codigo_producto` VARCHAR(15) NOT NULL COLLATE 'utf8mb4_general_ci',
	`cantidad` INT(11) NOT NULL,
	`precio_unidad` DECIMAL(15,2) NOT NULL,
	`numero_linea` SMALLINT(6) NOT NULL,
	PRIMARY KEY(`codigo_pedido`, `codigo_producto`) USING BTREE,
   INDEX `codigo_producto` (`codigo_producto`) USING BTREE,
   CONSTRAINT `detalle_pedido_ibfk_1` FOREIGN KEY(`codigo_pedido`) REFERENCES `jardineriaonline`.`pedido` (`codigo_pedido`) ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT `detalle_pedido_ibfk_2` FOREIGN KEY(`codigo_producto`) REFERENCES `jardineriaonline`.`producto` (`codigo_producto`) ON UPDATE RESTRICT ON DELETE RESTRICT
	)
	*/
        private int codigo_pedido;
        private string codigo_producto;
        private int cantidad;
        private decimal precio_unidad;
        private int numero_linea;

        // Albergará los datos del producto
        private Producto productoEntero;


        public int Codigo_pedido { get => codigo_pedido; set => codigo_pedido = value; }
        public string Codigo_producto { get => codigo_producto; set => codigo_producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Precio_unidad { get => precio_unidad; set => precio_unidad = value; }
        public int Numero_linea { get => numero_linea; set => numero_linea = value; }
        public Producto ProductoEntero { get => productoEntero; set => productoEntero = value; }

        public DetallePedido()
        {
        }

        public DetallePedido(int codigo_pedido, string codigo_producto, int cantidad, decimal precio_unidad, int numero_linea)
        {
            this.Codigo_pedido = codigo_pedido;
            this.Codigo_producto = codigo_producto;
            this.Cantidad = cantidad;
            this.Precio_unidad = precio_unidad;
            this.Numero_linea = numero_linea;
            // Le doy los datos al producto
            this.ProductoEntero = Producto.ListaProducto(codigo_producto)[0];
        }


        public override bool Equals(object obj)
        {
            return obj is DetallePedido pedido &&
                   Codigo_pedido == pedido.Codigo_pedido &&
                   Codigo_producto == pedido.Codigo_producto &&
                   Cantidad == pedido.Cantidad &&
                   Precio_unidad == pedido.Precio_unidad &&
                   Numero_linea == pedido.Numero_linea &&
                   EqualityComparer<Producto>.Default.Equals(ProductoEntero, pedido.ProductoEntero);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Codigo_pedido, Codigo_producto, Cantidad, Precio_unidad, Numero_linea, ProductoEntero);
        }

        public override string ToString()
        {
            return $"{Numero_linea,-7}{ProductoEntero.Nombre,-40}{Cantidad,-10}" +
                $"{Precio_unidad,-10}{Cantidad * Precio_unidad,-10}";
        }

        public static List<DetallePedido> ListaDetalle(int codigo)
        {
            List<DetallePedido> lista = new List<DetallePedido>(); // lo que devolverá
            // Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            MySqlDataReader lector;

            // Paso 3
            bd.Abrir();

            //Paso 4
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "SELECT * FROM detalle_pedido WHERE codigo_pedido = ?codigo_pedido ORDER by numero_linea";
            comando.Parameters.Add("?codigo_pedido", MySqlDbType.Int32).Value = codigo;
            // Paso 5
            lector = bd.EjecutarSelect(comando);

            // Paso 6 
            while (lector.Read())
            {
                lista.Add(new DetallePedido(lector.GetInt32("codigo_pedido"), lector.GetString("codigo_producto"),
                              lector.GetInt32("cantidad"), lector.GetDecimal("precio_unidad"),
                              lector.GetInt32("numero_linea")));
            }

            //Paso 7
            lector.Close();
            bd.Cerrar();

            return lista;
        }

        // CREAR LA FUNCION INSERTAR
        public int Insertar(int codigo)
        {
            // Hace la insert en la bd.
            int filasAfectadas = 0;
            //Pasos 1 y 2
            BasedeDatos bd = new BasedeDatos();
            // Paso 3
            bd.Abrir();

            // Paso 4
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO detalle_pedido (codigo_pedido,codigo_producto,cantidad,precio_unidad,numero_linea) " +
                               "VALUES(?codigo_pedido,?codigo_producto,?cantidad,?precio_unidad,?numero_linea)";

            cmd.Parameters.Add("?codigo_pedido", MySqlDbType.Int32).Value = codigo;
            cmd.Parameters.Add("?codigo_producto", MySqlDbType.VarChar).Value = this.Codigo_producto;
            cmd.Parameters.Add("?cantidad", MySqlDbType.Int32).Value = this.Cantidad;
            cmd.Parameters.Add("?precio_unidad", MySqlDbType.Decimal).Value = this.Precio_unidad;
            cmd.Parameters.Add("?numero_linea", MySqlDbType.Int32).Value = this.numero_linea;

            filasAfectadas = bd.EjecutarIUD(cmd);

            // Paso 7
            bd.Cerrar();

            return filasAfectadas;

        }

    }
}
