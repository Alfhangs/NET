
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Proyecto_Final
{
    class Program
    {
        public static void InsertarCliente()
        {
            Cliente cliente = new Cliente();
            Console.Write("Nombre cliente: ");
            while ((cliente.Nombre_cliente = Console.ReadLine()) == "")
            {
                Console.WriteLine("El nombre del cliente no puede estar vacío");
                Console.Write("Nombre cliente: ");
            }
            Console.Write("Nombre contacto: ");
            cliente.Nombre_contacto = Console.ReadLine();
            Console.Write("Apellido contacto: ");
            cliente.Apellido_contacto = Console.ReadLine();
            Console.Write("Teléfono: ");
            while ((cliente.Telefono = Console.ReadLine()) == "")
            {
                Console.WriteLine("El teléfono del cliente no puede estar vacío");
                Console.Write("Teléfono: ");
            }
            Console.Write("Fax: ");
            while ((cliente.Fax = Console.ReadLine()) == "")
            {
                Console.WriteLine("El Fax del cliente no puede estar vacío");
                Console.Write("Fax: ");
            }
            Console.Write("Direccion 1: ");
            while ((cliente.Linea_direccion1 = Console.ReadLine()) == "")
            {
                Console.WriteLine("La dirección del cliente no puede estar vacío");
                Console.Write("Dirección 1: ");
            }
            Console.Write("Dirección 2: ");
            cliente.Linea_direccion2 = Console.ReadLine();
            Console.Write("Ciudad: ");
            while ((cliente.Ciudad = Console.ReadLine()) == "")
            {
                Console.WriteLine("La ciudad del cliente no puede estar vacía");
                Console.Write("Ciudad: ");
            }
            Console.Write("Región: ");
            cliente.Region = Console.ReadLine();
            Console.Write("País: ");
            cliente.Pais = Console.ReadLine();
            Console.Write("Código Postal: ");
            cliente.Codigo_postal = Console.ReadLine();
            int codigoR;
            decimal limite;
            Console.Write("Código de su representante: ");
            if (Int32.TryParse(Console.ReadLine(), out codigoR))
                cliente.Codigo_empleado_rep_ventas = codigoR;
            else
                cliente.Codigo_empleado_rep_ventas = 0;

            Console.Write("Límite de crédito: ");
            if (Decimal.TryParse(Console.ReadLine(), out limite))
                cliente.Limite_credito = limite;
            else
                cliente.Limite_credito = 0;
            Console.Write("Contraseña: ");
            while ((cliente.Pass = Console.ReadLine()) == "")
            {
                Console.WriteLine("La contraseña no puede estar vacía");
                Console.Write("Contraseña: ");
            }
            try
            {
                int filas = cliente.Insertar();

                if (filas > 0)
                {
                    Console.WriteLine($"La inserción se ha realizado correctamente");
                    ListaIncidencias.AddIncidencia(DateTime.Now, $"Insertar cliente: {cliente}");
                }
                else
                {
                    Console.WriteLine($"La inserción NO se ha podido realizar");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error en la base de datos:{e.Message} ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error desconocido:{e.Message} ");
            }
            Console.ReadLine();
        }

        public static string RellenaCampoString(string texto, string dato)
        {
            string resultado;
            Console.WriteLine($"{texto}: {dato}");
            Console.Write($"Nuevo {texto}: ");
            resultado = Console.ReadLine();
            if (resultado != "")
                return resultado;
            return dato;
        }
        public static void ModificarCliente()
        {

            int codigo_empleado_rep_ventas;
            decimal limite_credito;
            string pass;

            // Pedir código al usuario
            // 2. ListaCliente(código)
            //Pedir datos (excepto código) pero escribimos el dato anterior y
            //si dan Enter se conservará. 
            // nombre cliente:  (Valor anterior) Antonio
            // nuevo Valor (Enter sin cambios)
            //if nombre!="" --> entonces hay cambio y sino conservo el valor anterior.

            int codigo = 0;
            Console.Write("Código de cliente a modificar: ");
            Int32.TryParse(Console.ReadLine(), out codigo);

            Cliente cliente = Cliente.ListaCliente(codigo)[0];

            cliente.Nombre_cliente = RellenaCampoString("Nombre cliente",cliente.Nombre_cliente);
            cliente.Nombre_contacto = RellenaCampoString("Nombre contacto",cliente.Nombre_contacto);
            cliente.Apellido_contacto = RellenaCampoString("Apellido contacto",cliente.Apellido_contacto);
            cliente.Telefono = RellenaCampoString("Teléfono",cliente.Telefono);
            cliente.Fax = RellenaCampoString("Fax", cliente.Fax);
            cliente.Linea_direccion1 = RellenaCampoString("Linea dirección 1",cliente.Linea_direccion1);
            cliente.Linea_direccion2 = RellenaCampoString("Linea direccion 2",cliente.Linea_direccion2);
            cliente.Ciudad = RellenaCampoString("Ciudad", cliente.Ciudad);
            cliente.Region = RellenaCampoString("Región", cliente.Region);
            cliente.Pais = RellenaCampoString("País", cliente.Pais);
            cliente.Codigo_postal = RellenaCampoString("Código Postal",cliente.Codigo_postal);

            Console.WriteLine($"Código de su representante: {cliente.Codigo_empleado_rep_ventas}");
            Console.Write("Nuevo código de representante: ");
            string codigoR = Console.ReadLine();
            if (codigoR != "")
            {
                Int32.TryParse(codigoR, out codigo_empleado_rep_ventas);
                cliente.Codigo_empleado_rep_ventas = codigo_empleado_rep_ventas;
            }

            Console.WriteLine($"Límite crédito: {cliente.Limite_credito}");
            Console.Write("Nuevo límite crédito: ");
            codigoR = Console.ReadLine();
            if (codigoR != "")
            {
                Decimal.TryParse(codigoR, out limite_credito);
                cliente.Limite_credito = limite_credito;
            }
            cliente.Pass = RellenaCampoString("Contraseña", cliente.Pass);

            try
            {
                int filas = cliente.Actualizar();

                if (filas > 0)
                {
                    Console.WriteLine($"La actualización se ha realizado correctamente");
                    ListaIncidencias.AddIncidencia(DateTime.Now, $"Modificar cliente: {cliente}");
                }
                else
                {
                    Console.WriteLine($"La actualización NO se ha podido realizar");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error en la base de datos:{e.Message} ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error desconocido:{e.Message} ");
            }
            Console.ReadLine();
        }
        public static void BorrarCliente()
        {
            int codigo = 0, filas = 0;
            Console.Write("Código de cliente a borrar: ");
            Int32.TryParse(Console.ReadLine(), out codigo);

            try
            {
                Cliente cliente = Cliente.ListaCliente(codigo)[0];
                if (cliente.Codigo_empleado_rep_ventas == 0)
                {

                    cliente.Borrar();
                }
                else
                {
                    Console.WriteLine("No se puede borrar si tiene representante (Modifique primero el cliente)");
                }

                if (filas > 0)
                {
                    Console.WriteLine($"El borrado se ha realizado correctamente");
                    ListaIncidencias.AddIncidencia(DateTime.Now, $"Borrado cliente {cliente}");
                }
                else
                {
                    Console.WriteLine($"El borrado NO se ha podido realizar");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error en la base de datos:{e.Message} ");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error desconocido:{e.Message} ");
            }

            Console.ReadLine();
        }
        public static void MostrarClientes()
        {
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consultar clientes");
            List<Cliente> lc = Cliente.ListaCliente();
            foreach (Cliente c in lc)
            {
                Console.WriteLine(c);
            }
            Console.ReadLine();
        }
        public static void MenuClientes()
        {
            string opcion;
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("********   1.CLIENTES  **********");
                Console.WriteLine("1. INSERTAR un CLIENTE");
                Console.WriteLine("2. MODIFICAR un CLIENTE");
                Console.WriteLine("3. BORRAR un CLIENTE");
                Console.WriteLine("4. MOSTRAR todos los CLIENTES");
                Console.WriteLine("0.  Volver al menú Principal");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        InsertarCliente();
                        break;
                    case "2":
                        ModificarCliente();
                        break;
                    case "3":
                        BorrarCliente();
                        break;
                    case "4":
                        MostrarClientes();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        break;
                }
            } while (!salir);
        }

        public static void MostrarPedido()
        {
            Console.WriteLine("Introduzca pedido a mostrar: ");
            int codigo = Convert.ToInt32(Console.ReadLine());
            Pedido.ListaPedido(codigo)[0].Mostrar();
            
            Console.ReadKey();
        }

        private static void RecogerDetalle() 
        {
            DetallePedido d = new DetallePedido();
            int c;
            Console.Write("Código de producto:");
            while ((d.Codigo_producto = Console.ReadLine()) == "")
            {
                Console.WriteLine("El código de producto no puede estar vacío");
                Console.Write("Estado del pedido : ");
            }
            Console.Write("Código de producto: ");
            // COMPROBAR CóDIGO DE PRODCUTO.
            while (!Int32.TryParse(Console.ReadLine(), out c))
            {

                Console.WriteLine("El código de cliente no puede estar vacío");
                Console.Write("Código de cliente: ");
            }
            d.Cantidad = c;


        }

        public static void InsertarPedido()
        {

            Pedido p = new Pedido();
            DateTime t;
            int c;
            int resultadoInsert = 0;
            Console.Write("La fecha del pedido: ");
            while (!DateTime.TryParse(Console.ReadLine(), out t ))
            {
                Console.WriteLine("La fecha del pedido no puede estar vacía");
                Console.Write("La fecha del pedido: ");
            }
            p.Fecha_pedido = t;

            Console.Write("La fecha esperada: ");
            while (!DateTime.TryParse(Console.ReadLine(), out t))
            {
                Console.WriteLine("La fecha esperada no puede estar vacía");
                Console.Write("La fecha esperada: ");
            }
            p.Fecha_esperada = t;

            Console.Write("La fecha entrega: ");
            while (!DateTime.TryParse(Console.ReadLine(), out t))
            {
                // Revisar fecha nula
                //Console.WriteLine("La fecha esperada no puede estar vacía");
                Console.Write("La fecha entrega: ");
            }
            p.Fecha_entrega = t;

            Console.Write("Estado del pedido :");
            while ((p.Estado = Console.ReadLine()) == "")
            {
                Console.WriteLine("El estado no puede estar vacío");
                Console.Write("Estado del pedido : ");
            }
            Console.Write("Comentarios :");
            p.Comentarios = Console.ReadLine();
            
            bool clienteCorrecto = false;
            do
            {
                Console.Write("Código de cliente: ");
                while (!Int32.TryParse(Console.ReadLine(), out c))
                {

                    Console.WriteLine("El código de cliente no puede estar vacío");
                    Console.Write("Código de cliente: ");
                }
                if (Cliente.ListaCliente(c).Count > 0)
                {
                    p.Codigo_cliente = c;
                    clienteCorrecto = true;
                }
            }
            while (clienteCorrecto == false);
            // AHORA INSERTAMOS LOS DETALLES DEL PEDIDO



            resultadoInsert = p.Insertar();


            if (resultadoInsert > 0)
            { Console.WriteLine("Pedido insertado con éxito"); }
            else { Console.WriteLine("Pedido no insertado."); }
            Console.ReadKey();
        }
        public static void MenuPedidos()
        {
            ListaIncidencias.AddIncidencia(DateTime.Now, "Menú Pedidos");
            string opcion;
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("****   2. PEDIDOS   ****");
                 
                Console.WriteLine("1. INSERTAR PEDIDO");
               /*Console.WriteLine("2. MODIFICAR PEDIDO");
                Console.WriteLine("3. BORRAR PEDIDO");*/
                Console.WriteLine("4. MOSTRAR PEDIDO");
                Console.WriteLine("0. Volver al menú anterior");
                opcion = Console.ReadLine();
                switch (opcion)
                {

                    case "1":
                        InsertarPedido();
                        break;
                    case "4":
                        MostrarPedido();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        break;
                }
            } while (!salir);
        }

        public static void ClientesSpain()
        {
            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT nombre_cliente, pais, region " +
                               "FROM cliente " +
                               "WHERE pais = 'SPAIN'";
            Console.WriteLine("Clientes de España");
            Console.WriteLine($"{"NOMBRE CLIENTE",-40}{"PAIS",15}{"REGION",25}");
            Console.WriteLine("------------------------------------------------------------------------------");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString("nombre_cliente"),-40}{lector.GetString("pais"),15}{lector.GetString("region"),25}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta clientes España");
            Console.ReadKey();
        }
        public static void ClientesMadrid()
        {
            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT nombre_cliente, pais, region FROM cliente WHERE pais = 'SPAIN' AND region = 'MADRID'";
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            Console.WriteLine("Clientes de Madrid");
            Console.WriteLine($"{"NOMBRE CLIENTE",-40}{"PAIS",15}{"REGION",25}");
            Console.WriteLine("--------------------------------------------------------------------------------");
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString(0),-40}{lector.GetString(1),15}{lector.GetString(2),25}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta clientes Madrid");
            Console.ReadKey();
        }
        public static void ClientesCredito()
        {
            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand("SELECT nombre_cliente, limite_credito FROM cliente WHERE nombre_cliente LIKE 'A%' AND limite_credito > 1000");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            Console.WriteLine("Clientes A... y crédito > 1000");
            Console.WriteLine($"{"NOMBRE CLIENTE",-40}{"CRÉDITO",15}");
            Console.WriteLine("------------------------------------------------------");
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString("nombre_cliente"),-40}{lector.GetDecimal("limite_credito"),15:F2}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta Clientes crédito");
            Console.ReadKey();
        }
        public static void StockInferior()
        {

            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand("SELECT codigo_producto, nombre, cantidad_en_stock FROM producto WHERE cantidad_en_stock< 50");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            Console.WriteLine("Stock inferior a 50");
            Console.WriteLine($"{"CÓDIGO",-15}{"NOMBRE",-50}{"CANTIDAD",10}");
            Console.WriteLine("------------------------------------------------------------------------------------");
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString("codigo_producto"),-15}{lector.GetString("nombre"),-50}{lector.GetInt32("cantidad_en_stock"),10}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta productos con stock inferior a 50");
            Console.ReadKey();
        }
        public static void ProductosOrdenados()
        {
            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT codigo_producto, nombre, cantidad_en_stock, precio_venta, (cantidad_en_stock * precio_venta) AS valor " +
                              "FROM producto order BY valor desc";
            Console.WriteLine("Productos ordenados");
            Console.WriteLine($"{"CÓDIGO",-15}{"NOMBRE",-50}{"CANTIDAD",10}{"PRECIO",10}{"VALOR",10}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            while (lector.Read())
            {
                string nombre = lector.GetString("nombre").Length > 50 ? lector.GetString("nombre").Substring(0, 46) + "..." : lector.GetString("nombre");
                Console.WriteLine($"{lector.GetString("codigo_producto"),-15}{nombre,-50}{lector.GetInt32("cantidad_en_stock"),10}{lector.GetDecimal("precio_venta"),10}{lector.GetDecimal("valor"),10}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta los productos ordenados");
            Console.ReadKey();
        }
        public static void Pedidos2008()
        {
            /* SELECT codigo_pedido, fecha_pedido, comentarios, nombre_cliente, telefono
                FROM pedido p, cliente c
                WHERE p.codigo_cliente = c.codigo_cliente
                AND YEAR(fecha_pedido) = 2008 */
            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT codigo_pedido, fecha_pedido, comentarios, nombre_cliente, telefono " +
                              "FROM pedido p, cliente c " +
                              "WHERE p.codigo_cliente = c.codigo_cliente " +
                              "AND YEAR(fecha_pedido) = 2008";
            Console.WriteLine("Pedidos 2008");
            Console.WriteLine($"{"CÓDIGO",-10}{"FECHA",-11}{"COMENTARIOS",-40}{"NOMBRE",-40}{"TELEFONO",-15}");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            while (lector.Read())
            {
                string comentarios = lector.IsDBNull(2) ? "" : lector.GetString("comentarios");
                comentarios = comentarios.Length > 40 ? comentarios.Substring(0, 36) + "..." : comentarios;
                Console.WriteLine($"{lector.GetString("codigo_pedido"),-10}{lector.GetDateTime("fecha_pedido").ToShortDateString(),-11}{comentarios,-40}{lector.GetString("nombre_cliente"),-40}{lector.GetString("telefono"),-15}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta pedidos 2008");
            Console.ReadKey();

        }
        public static void PedidosAdelantados()
        {
            /* SELECT codigo_pedido, fecha_entrega, fecha_esperada, nombre_cliente, telefono
                FROM pedido p, cliente c
                WHERE p.codigo_cliente = c.codigo_cliente
                AND fecha_entrega < fecha_esperada */

            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT codigo_pedido, fecha_entrega, fecha_esperada, nombre_cliente, telefono " +
                                "FROM pedido p, cliente c " +
                                "WHERE p.codigo_cliente = c.codigo_cliente " +
                                "AND fecha_entrega<fecha_esperada";
            Console.WriteLine("Pedidos Anticipados");
            Console.WriteLine($"{"CÓDIGO",-10}{"FECHA ENTREGA",-15}{"FECHA ESPERADA",-15}{"NOMBRE",-40}{"TELEFONO",-15}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString("codigo_pedido"),-10}{lector.GetDateTime("fecha_entrega").ToShortDateString(),-15}{lector.GetDateTime("fecha_esperada").ToShortDateString(),-15}{lector.GetString("nombre_cliente"),-40}{lector.GetString("telefono"),-15}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta pedidos entregados antes");
            Console.ReadKey();
        }
        public static void RepresentantesMadrid()
        {
            /* SELECT nombre, apellido1, apellido2, email
                FROM empleado
                WHERE puesto = 'Representante Ventas' 
                AND codigo_oficina = 'MAD-ES'
            */

            BasedeDatos bd = new BasedeDatos();
            bd.Abrir();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT nombre, apellido1, apellido2, email " +
                               "FROM empleado " +
                               "WHERE puesto = 'Representante Ventas' " +
                               "AND codigo_oficina = 'MAD-ES'";
            Console.WriteLine("Representantes de Madrid");
            Console.WriteLine($"{"NOMBRE",-25}{"APELLIDOS",-50}{"EMAIL",-25}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            MySqlDataReader lector = bd.EjecutarSelect(cmd);
            while (lector.Read())
            {
                Console.WriteLine($"{lector.GetString("nombre"),-25}{lector.GetString("apellido1") + "  " + lector.GetString("apellido2"),-50}{lector.GetString("email"),-25}");
            }
            lector.Close();
            bd.Cerrar();
            ListaIncidencias.AddIncidencia(DateTime.Now, "Consulta representantes de Madrid");
            Console.ReadKey();
        }
        public static void MenuConsultas()
        {
            ListaIncidencias.AddIncidencia(DateTime.Now, "Menú Consultas");
            string opcion;
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("********   3.CONSULTAS  **********");
                Console.WriteLine("1. CLIENTES ESPAÑA");
                Console.WriteLine("2. CLIENTES MADRID");
                Console.WriteLine("3. CLIENTES LÍMITE CRÉDITO MAYOR 10000 ");
                Console.WriteLine("4. STOCK INFERIOR A 50 UNIDADES");
                Console.WriteLine("5. PRODUCTOS ORDENADOS");
                Console.WriteLine("6. PEDIDOS AÑO 2008");
                Console.WriteLine("7. PEDIDOS ENTREGADOS ANTES DE LO ESPERADO");
                Console.WriteLine("8. LISTADO DE REPRESENTANTES DE LA OFICINA DE MADRID");
                Console.WriteLine("0.  Volver al menú Principal");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        ClientesSpain();
                        break;
                    case "2":
                        ClientesMadrid();
                        break;
                    case "3":
                        ClientesCredito();
                        break;
                    case "4":
                        StockInferior();
                        break;
                    case "5":
                        ProductosOrdenados();
                        break;
                    case "6":
                        Pedidos2008();
                        break;
                    case "7":
                        PedidosAdelantados();
                        break;
                    case "8":
                        RepresentantesMadrid();
                        break;
                    case "0":
                        salir = true;

                        break;
                    default:
                        break;
                }
            } while (!salir);
        }
        public static void MenuPrincipal()
        {
            ListaIncidencias.AddIncidencia(DateTime.Now, "MenuPrincipal");
            string opcion;
            bool salir = false;
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(20, 2);
                Console.WriteLine("**   MENÚ PRINCIPAL   **");
                Console.SetCursorPosition(20, 3);
                Console.WriteLine("     1. CLIENTES        ");
                Console.SetCursorPosition(20, 4);
                Console.WriteLine("     2. PEDIDOS         ");
                Console.SetCursorPosition(20, 5);
                Console.WriteLine("     3. CONSULTAS       ");
                Console.SetCursorPosition(20, 6);
                Console.WriteLine("     0. SALIR           ");
                Console.ResetColor();
                Console.SetCursorPosition(20, 10);
                Console.Write("Introduzca Opcion: ");

                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        MenuClientes();
                        break;
                    case "2":
                        MenuPedidos();
                        break;
                    case "3":
                        MenuConsultas();
                        break;
                    case "0":
                        salir = true;
                        ListaIncidencias.GuardaIncidencias();
                        break;
                    default:
                        break;
                }
            } while (!salir);
        }



        static void Main(string[] args)
        {   //este
            ListaIncidencias incidencias = new ListaIncidencias();

            ListaIncidencias.AddIncidencia(DateTime.Now, "Main");

            MenuPrincipal();

            //List<Oficina> lo;
            //lo = Oficina.ListarOficinas();
            //foreach (Oficina o in lo)
            //{
            //    Console.WriteLine(o.ToString());
            //}
            //Console.WriteLine("------------------------------------");
            //lo = Oficina.ListaOficina("BOS-USA");
            //foreach (Oficina o in lo)
            //{
            //    Console.WriteLine(o.ToString());
            //}

            //// Insertar
            //Oficina oficinaInsert = new Oficina();
            //oficinaInsert.Codigo_oficina = "AKA-ALI";
            //oficinaInsert.Ciudad = "ALICANTE";
            //oficinaInsert.Pais = "ESPAÑA";
            //oficinaInsert.Region = "Comunidad Valenciana";
            //oficinaInsert.Codigo_postal = "03001";
            //oficinaInsert.Telefono = "666777888";
            //oficinaInsert.Linea_direccion1 = "Dirección oficina 1";
            //oficinaInsert.Linea_direccion2 = "Justo al lado de mi casa";

            //try
            //{
            //    int filas = oficinaInsert.Insertar();

            //    if (filas > 0)
            //    { 
            //        Console.WriteLine($"La inserción se ha realizado correctamente"); 
            //    }
            //    else
            //    { 
            //        Console.WriteLine($"La inserción NO se ha podido realizar"); 
            //    }
            //}
            //catch (MySqlException e)
            //{
            //    Console.WriteLine($"Error en la base de datos:{e.Message} ");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Error descinocido:{e.Message} ");
            //}


            // Actualizar
            //Oficina oficinaUpdate = new Oficina();
            //oficinaUpdate.Codigo_oficina = "AKA-ALI";
            //oficinaUpdate.Ciudad = "MÁLAGA";
            //oficinaUpdate.Pais = "ESPAÑA";
            //oficinaUpdate.Region = "Malaga";
            //oficinaUpdate.Codigo_postal = "25005";
            //oficinaUpdate.Telefono = "666777888";
            //oficinaUpdate.Linea_direccion1 = "Dirección oficina 1 (málaga)";
            //oficinaUpdate.Linea_direccion2 = "Muy lejos de mi casa";

            //try
            //{
            //    int filas = oficinaUpdate.Actualizar();

            //    if (filas > 0)
            //    { 
            //        Console.WriteLine($"La actualización se ha realizado correctamente"); 
            //    }
            //    else
            //    { 
            //        Console.WriteLine($"La actualización NO se ha podido realizar"); 
            //    }
            //}
            //catch (MySqlException e)
            //{
            //    Console.WriteLine($"Error en la base de datos:{e.Message} ");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Error descinocido:{e.Message} ");
            //}


            //// Borrar
            //Oficina oficinaBorrar = new Oficina();
            //oficinaBorrar.Codigo_oficina = "AKA-ALI";
            //oficinaBorrar.Ciudad = "MÁLAGA";
            //oficinaBorrar.Pais = "ESPAÑA";
            //oficinaBorrar.Region = "Malaga";
            //oficinaBorrar.Codigo_postal = "25005";
            //oficinaBorrar.Telefono = "666777888";
            //oficinaBorrar.Linea_direccion1 = "Dirección oficina 1 (málaga)";
            //oficinaBorrar.Linea_direccion2 = "Muy lejos de mi casa";

            //try
            //{
            //    int filas = oficinaBorrar.Borrar();

            //    if (filas > 0)
            //    {
            //        Console.WriteLine($"El borrado se ha realizado correctamente");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"El borrado NO se ha podido realizar");
            //    }
            //}
            //catch (MySqlException e)
            //{
            //    Console.WriteLine($"Error en la base de datos:{e.Message} ");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Error descinocido:{e.Message} ");
            //}

            //MenuPrincipal();
            //Menú principal
            //1.Clientes --> Menú Clientes
            //1.Insertar Cliente
            //2.Modificar Cliente
            //3.Borrar Cliente
            //4.Mostrar Clientes
            //0.Volver al menú anterior
            //2.Pedidos --> Menú Pedidos
            //4. Mostrar Pedidos
            //0. Volver al menú anterior
            //3. Consultas --> Menú Consultas
            //1. Clientes país SPAIN
            //2. Clientes Madrid
            //3. Clientes A 
            //4. Stock inferior a 50 unidades
            //5. Productos ordenados
            //6. Pedidos año 2008
            //7. Pedidos fecha entrega anterior a la esperada
            //8. Representates oficina de Madrid
            //0. Salir
        }
    }
}
