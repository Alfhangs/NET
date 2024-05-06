using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Proyecto_Final
{
    public class Producto
    {

		/* 
		 CREATE TABLE `producto` (
		`codigo_producto` VARCHAR(15) NOT NULL COLLATE 'utf8mb4_general_ci',
		`nombre` VARCHAR(70) NOT NULL COLLATE 'utf8mb4_general_ci',
		`gama` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_general_ci',
		`dimensiones` VARCHAR(25) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
		`proveedor` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
		`descripcion` TEXT NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
		`cantidad_en_stock` SMALLINT(6) NOT NULL,
		`precio_venta` DECIMAL(15,2) NOT NULL,
		`precio_proveedor` DECIMAL(15,2) NULL DEFAULT NULL,
		PRIMARY KEY (`codigo_producto`) USING BTREE,
		INDEX `gama` (`gama`) USING BTREE,
		CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`gama`) REFERENCES `jardineriaonline`.`gama_producto` (`gama`) ON UPDATE RESTRICT ON DELETE RESTRICT
	) */
		private string codigo_producto;
		private string nombre;
		private string gama;
		private string dimensiones;
		private string preveedor;
		private string descripcion;
		private int cantidad_en_stock;
		private decimal precio_venta;
		private decimal precio_proveedor;

        public string Codigo_producto { get => codigo_producto; set => codigo_producto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Gama { get => gama; set => gama = value; }
        public string Dimensiones { get => dimensiones; set => dimensiones = value; }
        public string Preveedor { get => preveedor; set => preveedor = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Cantidad_en_stock { get => cantidad_en_stock; set => cantidad_en_stock = value; }
        public decimal Precio_venta { get => precio_venta; set => precio_venta = value; }
        public decimal Precio_proveedor { get => precio_proveedor; set => precio_proveedor = value; }

        //public string Codigo_Producto 
        //{ 
        //	get { return codigo_producto;}
        //	set {
        //		if (value != "VACIO")
        //			codigo_producto = value;
        //		else
        //			throw new ArgumentException();
        //	}		
        //}

        //public decimal Precio_Venta
        //{
        //	get { return precio_venta; }
        //	set
        //	{
        //		if (value >= 0)
        //			precio_venta = value;
        //		else
        //			throw new ArgumentException();
        //	}
        //}
        public Producto()
        {
        }

        public Producto(string codigo_producto, string nombre, string gama, string dimensiones, string preveedor, string descripcion, int cantidad_en_stock, decimal precio_venta, decimal precio_proveedor)
        {
            this.Codigo_producto = codigo_producto;
            this.Nombre = nombre;
            this.Gama = gama;
            this.Dimensiones = dimensiones;
            this.Preveedor = preveedor;
            this.Descripcion = descripcion;
            this.Cantidad_en_stock = cantidad_en_stock;
            this.Precio_venta = precio_venta;
            this.Precio_proveedor = precio_proveedor;
        }

        public override bool Equals(object obj)
        {
            return obj is Producto producto &&
                   Codigo_producto == producto.Codigo_producto &&
                   Nombre == producto.Nombre &&
                   Gama == producto.Gama &&
                   Dimensiones == producto.Dimensiones &&
                   Preveedor == producto.Preveedor &&
                   Descripcion == producto.Descripcion &&
                   Cantidad_en_stock == producto.Cantidad_en_stock &&
                   Precio_venta == producto.Precio_venta &&
                   Precio_proveedor == producto.Precio_proveedor;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Codigo_producto);
            hash.Add(Nombre);
            hash.Add(Gama);
            hash.Add(Dimensiones);
            hash.Add(Preveedor);
            hash.Add(Descripcion);
            hash.Add(Cantidad_en_stock);
            hash.Add(Precio_venta);
            hash.Add(Precio_proveedor);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{Codigo_producto}-{Nombre}-{Cantidad_en_stock}-{Precio_venta}";
        }


        public static List<Producto> ListaProducto (string codigo)
        {
            List<Producto> lista = new List<Producto>();
            MySqlDataReader lector;
            BasedeDatos bd = new BasedeDatos();

            bd.Abrir();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM producto WHERE codigo_producto = ?codigo_producto";
            cmd.Parameters.Add("?codigo_producto", MySqlDbType.VarChar).Value = codigo;

            lector = bd.EjecutarSelect(cmd);

            while (lector.Read())
            {
                lista.Add(new Producto(lector.GetString("codigo_producto"), lector.GetString("nombre"), lector.GetString("gama"),
                            lector.IsDBNull(3) ? "":lector.GetString("dimensiones"),
                            lector.IsDBNull(4) ? "" : lector.GetString("proveedor"),
                            lector.IsDBNull(5) ? "" : lector.GetString("descripcion"),
                            lector.GetInt32("cantidad_en_stock"),lector.GetDecimal("precio_venta"),
                            lector.IsDBNull(8) ? 0 : lector.GetDecimal("precio_proveedor"))
                    );
            }
            lector.Close();
            bd.Cerrar();
            return lista;

        }


    }
}
